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
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Helper;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using MediatR;
namespace RsCode.AspNetCore
{
    public static class RsCodeExtensions
    {
        public static void AddRsCode(this IServiceCollection services)
        {
            services.AddLogging();
       
                services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
                //         //添加内存缓存
                services.AddMemoryCaches();
            services.AddSingleton<CacheHelper>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddTransient<IdGenerate>();
            //解决中文被编码
            services.TryAddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //添加AI幂等处理

            //添加跨域支持

            services.AddControllersWithViews();
            services.AddControllers().AddControllersAsServices();
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.Filters.Add<AntiXSSAttribute>();
                config.Filters.Add<ModelValidateFilter>();
                config.Filters.Add<AppExceptionFilter>();
                config.Filters.Add<QpsAttribute>();
                config.OutputFormatters.Insert(0, new RsOutputFormatter());
                config.InputFormatters.Insert(0, new RsInputFormatter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            ;
            

            services.BuildDynamicProxyProvider();
        }
    }
}
