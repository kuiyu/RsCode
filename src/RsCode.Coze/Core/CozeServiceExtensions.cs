using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RsCode.Coze.Core;

namespace RsCode.Coze
{
    public static class CozeServiceExtensions
    {
        public static void AddCoze(this IServiceCollection services,IConfiguration configuration)
        { 
            services.Configure<List<CozeAppConfig>>(options =>configuration.GetSection("ByteDance:Coze").Bind(options)); 
        }
    }
}
