using Cards.Cache.Services.Abstractions;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Cards.Cache.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _cacheDatabase;

        public RedisCacheService(IDatabase database)
        {
            _cacheDatabase = database;
        }

        public async Task<T> GetDataAsync<T>(string key)
        {
            var keyValue = await _cacheDatabase.StringGetAsync(EnrichKey(key));

            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return default!;
            }
            return JsonSerializer.Deserialize<T>(keyValue.ToString())!;
        }

        public async Task<bool> SetDataAsync<T>(
            string key,
            T value,
            TimeSpan? absoluteExpiration = null,
            CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            return await _cacheDatabase.StringSetAsync(EnrichKey(key), JsonSerializer.Serialize(value), absoluteExpiration);
        }

        private static string EnrichKey(string key)
        {
            var context = "CardsDbUser";
            var stringBuilder = new StringBuilder().Append($"{context}:{key}");

            return stringBuilder.ToString();
        }
    }
}
