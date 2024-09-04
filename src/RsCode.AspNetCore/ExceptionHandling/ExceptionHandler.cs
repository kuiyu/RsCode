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

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Text.Json;

namespace RsCode.AspNetCore
{
    public interface IExceptionHandler
    {
        Task Handle(HttpContext context);
    }
    public class ExceptionHandler : IExceptionHandler 
    {
         ILogger Logger { get; set; } = NullLogger.Instance;

        public Task Handle(HttpContext context)
        {
            IActionResult result;

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null)
                return Task.CompletedTask;

            Logger.LogError(new EventId(ex.HResult),
                                ex.InnerException ?? ex,
                                 ex.InnerException != null ? ex.InnerException.Message : ex.Message
                                 );
                       
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                var options = new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    IgnoreNullValues = true,
                    WriteIndented = true,
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = false,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase //小写开头
                };
                options.WriteIndented = true;

                

                if (ex is AppException e)
                    result = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo(e.Status, e.Message), options)
                    };
                else if (ex is Exception err)
                    result = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo((int)HttpStatusCode.InternalServerError, err.Message), options)
                    };
                else
                {
                    result = new JsonResult("A server error occurred. Try again or contact the system administrator if the problem persists.")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            else
            {
                result = new RedirectResult("/error");
            }

         
 
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(context, routeData, actionDescriptor);

            return result.ExecuteResultAsync(actionContext);
        }
    }


    
}
