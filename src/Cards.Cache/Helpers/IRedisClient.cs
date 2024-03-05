using StackExchange.Redis;

namespace Cards.Cache.Helpers
{
    public interface IRedisClient
    {
        public IConnectionMultiplexer Connection { get; }

        public IDatabase Db { get; }
    }
}
