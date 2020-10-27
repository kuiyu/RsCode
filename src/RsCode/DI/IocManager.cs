using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rs.DI
{
    public static class IocManager
    {
        //public static IServiceProvider GetAutofacServiceProvider(this IServiceCollection services)
        //{
        //    var containerBuilder = new ContainerBuilder();
        //    string bin = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    Assembly[] assemblies = Directory.GetFiles(bin, "*.dll").Select(Assembly.LoadFrom).ToArray();

        //    //注册指定应用程序集生命周期为瞬态
        //    var transientType = typeof(ITransientDependency);
        //    containerBuilder.RegisterAssemblyTypes(assemblies)
        //        .Where(type => transientType.IsAssignableFrom(type) && !type.IsAbstract)
        //        .AsSelf().AsImplementedInterfaces()
        //        .PropertiesAutowired().InstancePerDependency();

        //    //注册指定应用程序集生命周期为单例
        //    var singleType = typeof(ISingletonDependency);
        //    containerBuilder.RegisterAssemblyTypes(assemblies)
        //        .Where(type => singleType.IsAssignableFrom(type) && !type.IsAbstract)
        //        .AsSelf().AsImplementedInterfaces()
        //        .PropertiesAutowired().SingleInstance();

        //    //注册指定应用程序集生命周期为Scoped;
        //    var scopedType = typeof(IScopedDependency);
        //    containerBuilder.RegisterAssemblyTypes(assemblies)
        //        .Where(type => scopedType.IsAssignableFrom(type) && !type.IsAbstract)
        //        .AsSelf().AsImplementedInterfaces()
        //        .PropertiesAutowired().InstancePerLifetimeScope();

        //    containerBuilder.Populate(services);
        //    var container = containerBuilder.Build();

        //    return new AutofacServiceProvider(container);
        //}
    }
}
