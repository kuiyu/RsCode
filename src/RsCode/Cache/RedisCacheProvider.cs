/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git

 * 文档 https://rscode.cn/
 */


using System;
using System.Threading.Tasks;

namespace RsCode.Cache
{
    public partial class RedisCacheProvider :ICacheProvider , IDisposable
    {
         
        public string CacheName { get; } = "redis";
        public CacheType CacheType { get; } = CacheType.Redis;

        

        public virtual T Get<T>(string key)
        {
           return RedisHelper.Get<T>(key); 
        }
        public virtual void Remove(string key)
        {
            RedisHelper.Del(key);
        }

        public virtual void Dispose()
        {
            
        }

        public void Set(string key, object obj, int seconds = 300)
        {
            SetAsync(key, obj, seconds).GetAwaiter().GetResult();
            
        }
        public async Task SetAsync(string key, object obj, int seconds = 300)
        {
            if (seconds == -1)
            {
                await RedisHelper.SetAsync(key, obj, -1);
            }
            else
            {
                await RedisHelper.SetAsync(key, obj, seconds);
            }
        }

        public void Set(string key, object obj, DateTime endDate)
        {
            SetAsync(key, obj, endDate).GetAwaiter().GetResult();
        }

        public async Task SetAsync(string key, object obj, DateTime endDate)
        {
            int expireSeconds = Convert.ToInt32((endDate - DateTime.Now).TotalSeconds);
            if (expireSeconds > 0)
              await  SetAsync(key, obj, expireSeconds);
        }


        public bool Exists(string key)
        {
            return RedisHelper.Exists(key); 
        }

        public object Get(string key)
        {
          return   GetAsync(key).GetAwaiter().GetResult();
        }

        

       

        public async Task<object> GetAsync(string key)
        {
            return await RedisHelper.GetAsync(key);
        }
    }
}
