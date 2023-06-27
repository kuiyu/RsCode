using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RsCode.AspNetCore;
using System;
using System.Collections.Generic;
using System.Text;
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
