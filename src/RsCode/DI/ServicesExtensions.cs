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
using Microsoft.Extensions.DependencyInjection;
using RsCode.DI;
using RsCode.Domain;



namespace RsCode
{



    public static class ServicesExtensions
    {
      
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
