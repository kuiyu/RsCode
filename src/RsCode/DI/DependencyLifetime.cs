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

namespace RsCode.DI
{
    
    public enum DependencyLifetime
    {
        /// <summary>
        /// 单例每次都用同一个对象
        /// </summary>
        Singleton,
        /// <summary>
        ///  同一个Lifetime生成的对象是同一个实例
        /// </summary>
        Scoped,
        /// <summary>
        /// 每次请求都创建一个新的对象
        /// </summary>
        Transient
    }
}
