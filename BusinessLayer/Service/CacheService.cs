using BusinessLayer.Cofiguration;
using BusinessLayer.Interface;
using StackExchange.Redis;

namespace BusinessLayer.Service
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;

        public CacheService(RedisConfig redisConfig)
        {
            _cache = redisConfig.Database;
        }

        public async Task<string> GetCachedData(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task SetCachedData(string key, string value)
        {
            await _cache.StringSetAsync(key, value, TimeSpan.FromMinutes(10)); // Cache for 10 mins
        }

        public async Task RemoveCachedData(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
