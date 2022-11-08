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

 * 文档 https://rscode.cn/
 */

using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RsCode.AspNetCore;
using System;

namespace RsCode
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 添加异常记录
        /// </summary>
        /// <param name="services"></param>
        public static void AddExceptionLogging(this IServiceCollection services)
        {
            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddDelegate(async (context, next) =>
                {
                    try
                    {
                        await context.Invoke(next);
                    }
                    catch (AppException e)
                    {
                        throw e;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(context.Implementation.GetType(), ex);
                        throw ex;
                    }
                });
            });
        }
    }
}
