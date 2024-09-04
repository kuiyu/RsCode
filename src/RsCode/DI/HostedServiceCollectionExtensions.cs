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
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RsCode.DI;

namespace RsCode
{
    /// <summary>
    /// Contains <see cref="IServiceCollection"/> extension methods to register all the <see cref="IHostedService"/>
    /// containing <see cref="HostedServiceAttribute"/> attribute at once.
    /// </summary>
    public static class HostedServiceCollectionExtensions
    {
        /// <summary>
        /// Add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The type to be extended.</param>
        /// <param name="scanAssembliesStartsWith">Assemblies to be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        public static void AddHostedServices(this IServiceCollection serviceCollection, params string[] scanAssembliesStartsWith)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            List<Assembly> assemblies = AssemblyHelper.GetLoadedAssemblies(scanAssembliesStartsWith);
            AddHostedServices(serviceCollection, assemblies);
        }

        /// <summary>
        /// Add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The type to be extended.</param>
        /// <param name="assemblyToBeScanned"><see cref="Assembly"/> to be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        public static void AddHostedServices(this IServiceCollection serviceCollection, Assembly assemblyToBeScanned)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            List<Assembly> assemblies = new List<Assembly> { assemblyToBeScanned };
            AddHostedServices(serviceCollection, assemblies);
        }

        /// <summary>
        /// Add all the <see cref="IHostedService"/> containing <see cref="HostedServiceAttribute"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The type to be extended.</param>
        /// <param name="assembliesToBeScanned">Assemblies to be scanned.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceCollection"/> is <see langword="null"/>.</exception>
        public static void AddHostedServices(this IServiceCollection serviceCollection, IEnumerable<Assembly> assembliesToBeScanned)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            List<Type> hostedServices = assembliesToBeScanned
                .SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsDefined(typeof(HostedServiceAttribute), false)).ToList();

            foreach (Type hostedService in hostedServices)
            {
                serviceCollection.TryAddEnumerable(new ServiceDescriptor(typeof(IHostedService), hostedService, ServiceLifetime.Singleton));
            }
        }
    }
}
