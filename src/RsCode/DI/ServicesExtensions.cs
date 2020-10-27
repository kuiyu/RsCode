using Microsoft.Extensions.DependencyInjection;
using PetaPoco;
using RsCode.Domain.Repositories;
using System.Collections.Generic;


namespace RsCode.DI
{



    public static class ServicesExtensions
    {
        public static Dictionary<string, IDatabaseBuildConfiguration> DbConfigs;

        
        /// <summary>
        /// 自动注入指定名称的程序集，仅适用于
        /// 继承IScopedDependency的 接口的实现
        /// 继承ITransientDependency的接口的实现
        /// 继承ISingletonDependency的接口的实现
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName">程序集名称</param>
        public static void AutoRegister(this IServiceCollection services, string assemblyName)
        { 


            foreach (var item in RegistrationUtils.GetClassName(assemblyName))
            { 

                foreach (var interfaceType in item.Value)
                {
                    if (typeof(IRepository<>).IsAssignableFrom(interfaceType))
                    {
                        continue;
                    }

                    if (typeof(IScopedDependency).IsAssignableFrom(interfaceType))
                        {
                            services.AddScoped(interfaceType, item.Key);
                        }
                        else
                          if (typeof(ITransientDependency).IsAssignableFrom(interfaceType))
                        {
                            services.AddTransient(interfaceType, item.Key);
                        }
                        else
                          if (typeof(ISingletonDependency).IsAssignableFrom(interfaceType))
                        {
                            services.AddSingleton(interfaceType, item.Key);
                        }
                        else
                        {
                            services.AddScoped(interfaceType, item.Key);
                        }

                     
                   
                }
            }       
          
        }
         
       

    }
}
