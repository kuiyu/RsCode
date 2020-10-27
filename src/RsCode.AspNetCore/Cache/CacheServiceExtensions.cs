using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RsCode.Cache;

using System;

namespace RsCode.AspNetCore.Cache
{
    public static class CacheServiceExtensions
    {
        //need Caching.CSRedis
        public static void  AddRedisCaches(this IServiceCollection services, IConfiguration configuration)
        {
            string redisServer = configuration.GetValue<string>("RedisServer:Server");
            var csredis = new CSRedis.CSRedisClient(redisServer);
            RedisHelper.Initialization(csredis);

            if (RedisHelper.Instance == null)
                throw new Exception("redis config not fund");

            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            services.AddSingleton<ICacheProvider, RedisCacheProvider>();
        }


        public static void AddRedisCaches(this IServiceCollection services, IConfiguration configuration, CSRedisClient csredis)
        {  
            RedisHelper.Initialization(csredis);

            if (RedisHelper.Instance == null)
                throw new Exception("redis config not fund");

            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            services.AddSingleton<ICacheProvider, RedisCacheProvider>();
        }

        public static void AddMemoryCaches(this IServiceCollection services)
        {
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
            services.AddMemoryCache();
        }

        public static void AddMemcachedCaches(this IServiceCollection services)
        {
            services.AddSingleton<ICacheProvider, MemcachedCacheProvider>();
        }
    }
}
