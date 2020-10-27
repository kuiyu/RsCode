using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RsCode.AspNetCore.Session
{
    public static  class HttpContextAccessorExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        }

    }
}
