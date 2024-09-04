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
