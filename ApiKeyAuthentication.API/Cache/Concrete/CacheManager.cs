using ApiKeyAuthentication.API.Cache.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlTypes;

namespace ApiKeyAuthentication.API.Cache.Concrete
{
    public class CacheManager : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CacheManager(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public TObject Get<TObject>(object key)
        {
            if (key is null || !_memoryCache.TryGetValue(key, out TObject value))
                return default;

            return value;
        }

        public void AddCache(object key, object obj)
        {
            _memoryCache.Set(key, obj, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.Parse(_configuration["Cache:SlidingExpiration"]),
                AbsoluteExpiration = DateTime.Now.AddMinutes(double.Parse(_configuration["Cache:AbsoluteExpirationMinutes"])),
            });
        }

        public void RemoveCache(object key)
        {
            _memoryCache.Remove(key);
        }
    }
}
