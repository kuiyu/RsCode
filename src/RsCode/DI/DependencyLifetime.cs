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
