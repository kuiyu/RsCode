
using System;
using Microsoft.Extensions.DependencyInjection;

namespace RsCode
{
    /// <summary>
    /// 瞬时（Transient）生命周期服务在它们每次请求时被创建。
    ///  这一生命周期适合轻量级的，无状态的服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TransientServiceAttribute : Attribute
    {
    }
}
