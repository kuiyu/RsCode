using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RsCode.Helper;

namespace RsCode.AspNetCore
{
    public static class RsCodeExtensions
    {
        public static void AddRsCode(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddMvc(config => {
                config.Filters.Add<AntiXSSAttribute>();
                config.Filters.Add<ModelValidateFilter>();
                config.OutputFormatters.Insert(0, new RsOutputFormatter());
                config.InputFormatters.Insert(0, new RsInputFormatter());
            }).AddControllersAsServices()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddExceptionLogging();
            //添加内存缓存
            services.AddMemoryCaches();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IdGenerate>();
            
        }
    }
}
