/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using AspectCore.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Helper;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace RsCode.AspNetCore
{
    public static class RsCodeExtensions
    {
        public static void AddRsCode(this IServiceCollection services)
        {
            services.BuildDynamicProxyProvider();
            services.AddLogging();
            services.AddMvc(config => {
                config.RespectBrowserAcceptHeader = true;
                config.Filters.Add<AntiXSSAttribute>();
                config.Filters.Add<ModelValidateFilter>();
                config.Filters.Add<AppExceptionFilter>();
                config.Filters.Add<QpsAttribute>();
                config.OutputFormatters.Insert(0, new RsOutputFormatter());
                config.InputFormatters.Insert(0, new RsInputFormatter());
            }).AddControllersAsServices()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            ;
           
           services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
       
            services.AddExceptionLogging();
            //添加内存缓存
            services.AddMemoryCaches();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddTransient<IdGenerate>();
			//解决中文被编码
			services.TryAddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //添加AI幂等处理

            //添加跨域支持

           
		}
    }
}
