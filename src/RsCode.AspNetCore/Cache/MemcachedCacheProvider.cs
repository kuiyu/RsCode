using Enyim.Caching;
using System;
using System.Threading.Tasks;

namespace RsCode.Cache
{
    public class MemcachedCacheProvider : ICacheProvider
    {
        public string CacheName { get; } = "memcached";

        public CacheType CacheType { get;} = CacheType.Memcached;

        IMemcachedClient cache;
        public MemcachedCacheProvider(IMemcachedClient _cache)
        {
            cache = _cache;
        }
        public bool Exists(string key)
        {
            if (Get(key) == null) return false;
            return true;
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }
        public async Task<object> GetAsync(string key)
        {
            return await Task.Run(() => cache.Get(key));
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }


        
        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set(string key, object obj, int seconds = 300)
        {
            SetAsync(key, obj, seconds).GetAwaiter().GetResult();
        }
        public async Task SetAsync(string key, object obj, int seconds = 300)
        {
            if (seconds == -1)
            {
                seconds = (int)(DateTime.MaxValue - DateTime.Now).TotalSeconds;
                await cache.SetAsync(key, obj, seconds); 
             }
            else
            {
                await cache.SetAsync(key, obj, seconds);
            }
        }

        public void Set(string key, object obj, DateTime endDate)
        {
            SetAsync(key, obj, endDate).GetAwaiter().GetResult();
        }
        public async Task SetAsync(string key, object obj, DateTime endDate)
        {
            double seconds = (endDate - DateTime.Now).TotalSeconds;
            if (seconds > 0)
            {
               await SetAsync(key, obj, (int)seconds);
            }
        } 

        
    }
}
