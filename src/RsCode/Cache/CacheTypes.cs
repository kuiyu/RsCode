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
