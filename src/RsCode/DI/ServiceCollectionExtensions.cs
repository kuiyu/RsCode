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
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RsCode.DI;

namespace RsCode
{
    /// <summary>
    /// Contains <see cref="IServiceCollection"/> extension methods for dynamic service registration.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <para>
        /// 1. This will also add all services containing any of the <see cref="ScopedServiceAttribute"/>, <see cref="TransientServiceAttribute"/> and <see cref="SingletonServiceAttribute"/> attributes
        /// to the dependency injection container.
        /// </para>
        /// <para>
        /// 2. This will also add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to
        /// the dependency injection container.
        /// </para>
        /// </summary>
        /// <param name="serviceCollection">The type that has been extended.</param>
        /// <param name="scanAssembliesStartsWith">Assembly name starts with any of the provided strings will only be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        public static void AutoInject(this IServiceCollection serviceCollection, params string[] scanAssembliesStartsWith)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            List<Assembly> assemblies = AssemblyHelper.GetLoadedAssemblies(scanAssembliesStartsWith);
            AutoInject(serviceCollection, assemblies);
        }

        /// <summary>
        /// <para>
        /// 1. This will also add all services containing any of the <see cref="ScopedServiceAttribute"/>, <see cref="TransientServiceAttribute"/> and <see cref="SingletonServiceAttribute"/> attributes
        /// to the dependency injection container.
        /// </para>
        /// <para>
        /// 2. This will also add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to
        /// the dependency injection container.
        /// </para>
        /// </summary>
        /// <param name="serviceCollection">The type that has been extended.</param>
        /// <param name="assemblyToBeScanned">The <see cref="Assembly"/> will only be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="assemblyToBeScanned"/> is <see langword="null"/>.</exception>
        public static void AutoInject(this IServiceCollection serviceCollection, Assembly assemblyToBeScanned)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (assemblyToBeScanned == null)
            {
                throw new ArgumentNullException(nameof(assemblyToBeScanned));
            }

            List<Assembly> assemblies = new List<Assembly> { assemblyToBeScanned };
            AutoInject(serviceCollection, assemblies);
        }

        /// <summary>
        /// <para>
        /// 1. This will also add all services containing any of the <see cref="ScopedServiceAttribute"/>, <see cref="TransientServiceAttribute"/> and <see cref="SingletonServiceAttribute"/> attributes
        /// to the dependency injection container.
        /// </para>
        /// <para>
        /// 2. This will also add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to
        /// the dependency injection container.
        /// </para>
        /// </summary>
        /// <param name="serviceCollection">The type that has been extended.</param>
        /// <param name="assembliesToBeScanned">The <see cref="IEnumerable{T}"/> of <see cref="Assembly"/> which will be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="assembliesToBeScanned"/> is <see langword="null"/>.</exception>
        public static void AutoInject(this IServiceCollection serviceCollection, IEnumerable<Assembly> assembliesToBeScanned)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (assembliesToBeScanned == null)
            {
                throw new ArgumentNullException(nameof(assembliesToBeScanned));
            }

            serviceCollection.AddServicesWithAttributeOfType<TransientServiceAttribute>(assembliesToBeScanned);
            serviceCollection.AddServicesWithAttributeOfType<ScopedServiceAttribute>(assembliesToBeScanned);
            serviceCollection.AddServicesWithAttributeOfType<SingletonServiceAttribute>(assembliesToBeScanned);

            serviceCollection.AddHostedServices(assembliesToBeScanned);
        }
    }
}
