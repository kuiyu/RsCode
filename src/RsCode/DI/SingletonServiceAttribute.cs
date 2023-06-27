
using System;
using Microsoft.Extensions.DependencyInjection;

namespace RsCode
{
    /// <summary>
    /// 单例（Singleton）生命周期服务在它们第一次被解析时创建，并且每个后续解析将使用相同的实例。
    /// 如果你的应用程序需要单例行为，建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SingletonServiceAttribute : Attribute
    {
    }
}
