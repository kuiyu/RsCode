using RsCode.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsCode
{
    public partial class CacheHelper 
    {
        public CacheHelper(IEnumerable<ICacheProvider> _Caches)
        {
            Caches = _Caches;
            SetCache(CacheType.Memory);
        }
       
         IEnumerable<ICacheProvider> Caches { get; set; }

        ICacheProvider cache;
        public void SetCache(CacheType cacheType)
        {
            cache=Caches.FirstOrDefault(x=>x.CacheType==cacheType);
        }
        

        public bool Exists(string key)
        {
            return cache.Exists(key);
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public Task<object> GetAsync(string key)
        {
            return cache.GetAsync(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set(string key, object obj, int seconds = 300)
        {
            cache.Set(key, obj, seconds);   
        }

        public void Set(string key, object obj, DateTime endDate)
        {
            cache.Set(key,obj, endDate);    
        }

        public Task SetAsync(string key, object obj, int seconds = 300)
        {
            return cache.SetAsync(key,obj,seconds);
        }

        public Task SetAsync(string key, object obj, DateTime endDate)
        {
            return cache.SetAsync(key, obj, endDate);
        }
    }
}
