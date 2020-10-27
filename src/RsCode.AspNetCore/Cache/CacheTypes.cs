using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Cache
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        Memory = 1,
        /// <summary>
        /// redis缓存
        /// </summary>
        Redis = 2,
        /// <summary>
        /// memcached缓存
        /// </summary>
        Memcached = 3,
        /// <summary>
        /// sqlserver缓存
        /// </summary>
        SqlServer = 4

    }
}
