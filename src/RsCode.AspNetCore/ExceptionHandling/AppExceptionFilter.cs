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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

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

            var options = new JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition= System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
                WriteIndented = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase //小写开头
            }; options.WriteIndented = true;

            if (api != null)
            {
               

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
                var e=context.Exception as AppException;
                if(e!=null)
                {
                    var err = e.Message;
                    context.Result = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo(200, err), options)
                    };

                }else
                {
                    var err = context.Exception; 
                    context.Result = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json;charset=utf-8",
                        Content = JsonSerializer.Serialize(new ReturnInfo(200, err.Message), options)
                    };
                }
              
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
