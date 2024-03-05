using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Cards.Cache.Services.Abstractions;
using Cards.Cache.Services;

namespace Cards.Cache.Extensions
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedis(configuration);

            services.AddScoped<IRedisCacheService, RedisCacheService>();

            services.AddSingleton<IDatabase>(provider => {
                // Add your Redis connection logic here
                var redis = ConnectionMultiplexer.Connect("cardsCache:6379");
                return redis.GetDatabase();
            });

            return services;
        }
    }
}
