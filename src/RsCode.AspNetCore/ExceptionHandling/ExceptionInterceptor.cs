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

using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RsCode.AspNetCore;
using System;
using System.Threading.Tasks;

namespace RsCode.Exceptions
{
	public class ExceptionInterceptor : AbstractInterceptorAttribute
    {
        [FromServiceContext]
        public ILogger<ExceptionInterceptor> log { get; set; }
        public override async  Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppException e)
            {
                log.LogInformation(System.Text.Json.JsonSerializer.Serialize(e));
                await HandleExceptionAsync(context, next);
            }
            catch(Exception ex)
            {
                log.LogInformation(System.Text.Json.JsonSerializer.Serialize(ex));
                throw ex;
            }
        }

        async Task HandleExceptionAsync(AspectContext context, AspectDelegate next)
        {
            var value = context.ReturnValue;
           
        }

        bool IsAjax(HttpRequest request)
        {
            bool result = false;

            var xreq = request.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = request.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
    }
}
