﻿using Cards.Cache.Configurations;
using Cards.Cache.Healthchecks;
using Cards.Cache.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Cache.Extensions
{
    public static class RedisDependencyInjectionExtensions
    {
        private const string RedisSection = "Redis";

        private static IServiceCollection AddRedisOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection(RedisSection);
            services.Configure<RedisCacheSettings>(settings);

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisOptions(configuration);
            services.AddSingleton<IRedisClient, RedisConnectionClient>();

            services.AddSingleton((sp) => sp.GetRequiredService<IRedisClient>().Connection);

            services.AddHealthChecks().AddCheck<RedisCacheHealthCheck>("Redis", tags: new string[] { "readiness" });

            return services;
        }
    }
}
