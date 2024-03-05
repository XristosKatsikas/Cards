using Cards.Cache.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Cards.Cache.Healthchecks
{
    public class RedisCacheHealthCheck : IHealthCheck
    {
        private readonly RedisCacheSettings _settings;

        public RedisCacheHealthCheck(IOptions<RedisCacheSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect(_settings.ConnectionString);
                var db = redis.GetDatabase();

                var result = await db.PingAsync();
                if (result < TimeSpan.FromSeconds(5))
                {
                    return await Task.FromResult(
                        HealthCheckResult.Healthy());
                }

                return await Task.FromResult(
                    HealthCheckResult.Unhealthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(
                    HealthCheckResult.Unhealthy(e.Message));
            }
        }
    }
}
