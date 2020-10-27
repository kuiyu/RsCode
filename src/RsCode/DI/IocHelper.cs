using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Rs.DI
{




    public class IocHelper : IIocHelper
    { 
        static IocHelper Instance { get; set; }

        static IocHelper()
        {
            Instance = new IocHelper();
        }

        public IocHelper()
        {
            if (_builder == null)
                _builder = new ContainerBuilder();
        }
         
        IContainer container;
        public IContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = _builder.Build();
                }
                return container;
            }

            private set
            {
                container = _builder.Build();
            }
        }
 
        private static ContainerBuilder _builder;
 
        //要注册的程序集 或程序
        public static IServiceProvider GetServiceProvider(Action<IocHelper> register)
        {
            register(Instance);
            return new AutofacServiceProvider(Instance.container);
        }

         
        public static void Register<T>()
        {
            _builder.RegisterType<T>();
        }


        public static void Register<TType, TImpl>(DependencyLifetime lifetime = DependencyLifetime.Singleton)
            where TType : class
            where TImpl : class, TType
        {

            switch (lifetime)
            {
                case DependencyLifetime.Singleton:
                    _builder.RegisterType<TImpl>().As<TType>().SingleInstance();
                    break;
                case DependencyLifetime.Scoped:
                    _builder.RegisterType<TImpl>().As<TType>().InstancePerLifetimeScope();
                    break;
                case DependencyLifetime.Transient:
                    _builder.RegisterType<TImpl>().As<TType>().InstancePerDependency();
                    break;
                default:
                    _builder.RegisterType<TImpl>().As<TType>().SingleInstance();
                    break;
            }



        }
        public static void Register(object obj)
        {
            _builder.RegisterInstance(obj);
        }

        public static void RegisterAssembly(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly);
        }

        public static void RegisterAssemblys(Assembly[] Assemblys)
        {
            _builder.RegisterAssemblyTypes(Assemblys);
        }

        public static void Register<TImpl, TType>(string name)
            where TType : class
            where TImpl : class, TType
        {
            _builder.RegisterType<TImpl>().Named<TType>(name);
        }

        public static T GetService<T>(string name)
        {
            return Instance.Container.ResolveNamed<T>(name);
        }

        public static T GetService<T>()
        {
            return Instance.Container.Resolve<T>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }

    
}
