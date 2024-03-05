using Cards.Cache.Services.Abstractions;
using Cards.Core;
using StackExchange.Redis;
using System.Text;

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

            return string.IsNullOrWhiteSpace(keyValue) ? default : keyValue.ToString().FromJson<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> SetDataAsync<T>(
            string key,
            T value,
            TimeSpan? absoluteExpiration = null,
            CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            return await _cacheDatabase.StringSetAsync(EnrichKey(key), value!.ToJson(), absoluteExpiration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string EnrichKey(string key)
        {
            var context = "CardsDbUser";
            var stringBuilder = new StringBuilder().Append($"{context}:{key}");

            return stringBuilder.ToString();
        }
    }
}
