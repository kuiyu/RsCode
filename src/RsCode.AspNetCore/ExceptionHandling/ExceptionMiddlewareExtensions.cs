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
using Microsoft.AspNetCore.Builder;
using System;

namespace RsCode.AspNetCore
{
    public static class ExceptionMiddlewareExtensions
    {
        
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app, ExceptionHandlerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<ExceptionHandlerMiddleware>(options);
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app, string errorHandlingPath)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var options = new ExceptionHandlerOptions
            {
                ExceptionHandlingPath = new Microsoft.AspNetCore.Http.PathString(errorHandlingPath)
            };
            return app.UseMiddleware<ExceptionHandlerMiddleware>(options);
        }
    }
}
