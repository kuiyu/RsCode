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
