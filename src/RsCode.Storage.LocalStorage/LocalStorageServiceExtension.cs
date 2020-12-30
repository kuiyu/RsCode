/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RsCode.Storage.LocalStorage
{
    public static  class LocalStorageServiceExtension
    {
        public static void AddLocalStorage(this IServiceCollection services,IConfiguration configuration)
        { 
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<LocalOptions>();
            services.Configure<LocalOptions>(configuration.GetSection("LocalStorage"));
            
            services.AddScoped<IStorageProvider, LocalStorageService>();
        }
    }
}
