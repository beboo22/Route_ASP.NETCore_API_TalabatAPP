namespace Talabat.Core.ServiceContract
{
    public interface ICacheService
    {
        Task CreateCache(string CacheKey, object Response, TimeSpan time);
        Task<string?> GetFromCacheAsync(string CacheKey);
    }
}
