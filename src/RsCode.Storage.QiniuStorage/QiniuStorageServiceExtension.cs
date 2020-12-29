using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Storage.QiniuStorage;

namespace RsCode.Storage
{
    public static class QiniuStorageServiceExtension
    {
        public static void AddQiniuStorage(this IServiceCollection services, IConfiguration configuration,int ExpireSeconds=600)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<QiniuOptions>();
            services.Configure<QiniuOptions>(configuration.GetSection("QiniuStorage"));
            
            services.AddScoped<IStorageProvider, QiniuStorageProvider>();
            
        }
    }
}
