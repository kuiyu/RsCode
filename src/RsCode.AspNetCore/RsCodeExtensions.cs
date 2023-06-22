﻿/*
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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                config.Filters.Add<AppExceptionFilter>();
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddTransient<IdGenerate>();
        }
    }
}
