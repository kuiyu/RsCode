/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace RsCode.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public string CacheName { get; } = "memory";
        public CacheType CacheType { get;  } = CacheType.Memory;


        IMemoryCache cache;
        
        public MemoryCacheProvider(IMemoryCache _memoryCache)
        {
            cache = _memoryCache;
        }
        public T Get<T>(string key)
        {
            T value=default(T);
            cache.TryGetValue<T>(key, out value);
            return value;
        }
        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public async Task SetAsync(string key,object obj,int seconds=300)
        {
            await Task.Run(() => Set(key, obj, seconds));
        }

        public void Set(string key, object obj, int seconds = 300)
        {
            if (seconds == -1)
            {
                cache.Set(key, obj, DateTime.MaxValue);
            }
            else
            {
                DateTimeOffset dateTimeOffset = DateTime.Now.AddSeconds(seconds);
                cache.Set(key, obj, dateTimeOffset);
            }
        }


        public async Task SetAsync(string key, object obj, DateTime endDate)
        {
            await Task.Run(() => Set(key, obj, endDate));
        }

        public void Set(string key, object obj, DateTime endDate)
        {
            double seconds = (endDate - DateTime.Now).TotalSeconds;
            if (seconds > 0)
            {
                Set(key, obj, (int)seconds);
            }
        }
 

        public bool Exists(string key)
        {
            if (Get(key) != null)
                return true;
            return false;
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }
         
        public async Task<object>GetAsync(string key)
        {
            return await Task.Run(() => Get(key));
        }
 
    }

    
}
