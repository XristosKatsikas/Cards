using Cards.Cache.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Cards.Cache.Helpers
{
    public class RedisConnectionClient : IRedisClient
    {
        private readonly ILogger<RedisConnectionClient> _logger;
        private readonly IOptions<RedisCacheSettings> _redisSettings;

        private static IConnectionMultiplexer? _connection;
        private readonly static object _connectionLock = new object();
        private static int _reset;

        public IConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null || _reset == 1)
                {
                    lock (_connectionLock)
                    {
                        if (_connection == null || Volatile.Read(ref _reset) == 1)
                        {
                            _connection = CreateConnection();
                        }
                    }
                }

                return _connection;
            }
        }

        public IDatabase Db
        {
            get => Connection.GetDatabase(_redisSettings.Value.Database);
        }

        public RedisConnectionClient(ILogger<RedisConnectionClient> logger, IOptions<RedisCacheSettings> redisSettings)
        {
            _logger = logger;
            _redisSettings = redisSettings;
        }

        private ConnectionMultiplexer CreateConnection()
        {
            var configString = _redisSettings.Value.ConnectionString;

            var opts = ConfigurationOptions.Parse(configString);

            opts.ClientName = $"{Environment.MachineName}_{DateTime.UtcNow:s}";
            opts.KeepAlive = 60;
            opts.SyncTimeout = 15000;
            opts.ConnectTimeout = 15000;
            opts.AllowAdmin = true;
            opts.AbortOnConnectFail = false;

            if (!string.IsNullOrEmpty(_redisSettings.Value.Password))
            {
                opts.Password = _redisSettings.Value.Password;
            }

            Interlocked.Exchange(ref _reset, 0);
            var connection = ConnectionMultiplexer.Connect(opts);

            Task.Delay(TimeSpan.FromSeconds(60))
                .ContinueWith(_ => {
                    if (_connection != null && !_connection.IsConnected)
                    {
                        lock (_connectionLock)
                        {
                            if (_connection != null && !_connection.IsConnected)
                            {
                                _connection = null;
                            }
                        }
                    }
                });

            connection.InternalError += (o, e) => {
                _logger.LogError(e.Exception, "redis connection error: {0} {1} {2}", e.Origin, e.ConnectionType, e!.EndPoint!.Serialize().ToString());
                Interlocked.Exchange(ref _reset, 1);
            };

            return connection;
        }
    }
}
