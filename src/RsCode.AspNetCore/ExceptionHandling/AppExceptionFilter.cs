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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// controller action异常过滤器
    /// </summary>
    public class AppExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {

        ILoggerFactory _loggerFactory;
        ExceptionHandlerOptions options;
        public AppExceptionFilter(ILoggerFactory loggerFactory,IOptions< ExceptionHandlerOptions> options)
        {
            _loggerFactory = loggerFactory; 
            this.options = options.Value;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled) return;
            var api = context.Filters.FirstOrDefault(f => f.GetType() == typeof(ApiControllerAttribute));
            if (api != null)
            {
                var options = new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    IgnoreNullValues = true,
                    WriteIndented = true,
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = false,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase //小写开头
                }; options.WriteIndented = true;

                if (context.Exception is AppException)
                {
                    var e = context.Exception as AppException;

                    context.Result = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo(e.Status, e.Message), options)
                    };
                }
                else
                {
                    var act = context.Exception.TargetSite.ReflectedType;
                    var logger = _loggerFactory.CreateLogger(act);
                    var ex = context.Exception;
                    logger.LogError(new EventId(context.Exception.HResult),
                                 ex.InnerException ?? ex,
                                  ex.InnerException != null ? ex.InnerException.Message : ex.Message
                                  );

                    context.Result = new ContentResult
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo((int)HttpStatusCode.InternalServerError, $"{context.Exception.Message}"), options)
                    };
                }
            }
            else
            {
                context.Result = new RedirectResult(options.ExceptionHandlingPath.Value);
            }

            context.ExceptionHandled = true;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }

        Task IAsyncExceptionFilter.OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }

       
    }



}
