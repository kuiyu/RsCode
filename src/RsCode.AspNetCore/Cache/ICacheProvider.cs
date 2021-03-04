using System;
using System.Threading.Tasks;
using RsCode.Cache;
using RsCode.DI;

namespace RsCode
{
    /// <summary>
    /// 缓存接口定义
    /// </summary>
   public  interface ICacheProvider:ISingletonDependency
    {
        CacheType CacheType { get; }
        string CacheName { get; }

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">要缓存的数据</param>
        /// <param name="seconds">默认缓存时长300秒</param>
        void Set(string key, object obj, int seconds=300);

        Task SetAsync(string key, object obj, int seconds = 300);

        /// <summary>
        /// 缓存数据到指定时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="endDate">结束时间</param>
        void Set(string key, object obj, DateTime endDate);

        Task SetAsync(string key, object obj, DateTime endDate);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        bool Exists(string key);
   

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        object Get(string key);

        Task<object> GetAsync(string key);

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        T Get<T>(string key); 

        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

       
     
    }
}
