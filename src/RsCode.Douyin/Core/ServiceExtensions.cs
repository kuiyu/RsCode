/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
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
using Microsoft.Extensions.Options;
using RsCode.Douyin.Core;

namespace RsCode.Douyin
{
    public static class ServiceExtensions
    {
        public static void AddDouyin(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient<DouyinHttpClient>();
            services.AddTransient<DouyinHttpClientHandler>();
            services.AddScoped<IDouyinClient, DouyinClient>();


            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("douyin.json", optional: true, reloadOnChange: true);
            var config = configBuilder.Build();

            var app = config.GetSection("app");
            services.Configure<List<DouyinOptions>>(options => config.GetSection("app").Bind(options));

            //增加内存缓存
            services.AddMemoryCache();
        }
    }
}
