namespace Cards.Cache.Services.Abstractions
{
    public interface IRedisCacheService
    {
        Task<T> GetDataAsync<T>(string key);

        Task<bool> SetDataAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, CancellationToken token = default);
    }
}
