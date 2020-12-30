/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RsCode.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        ILogger log;
        public ExceptionHandlerMiddleware(RequestDelegate rd, ILogger<ExceptionHandlerMiddleware> _log)
        {
            requestDelegate = rd;
            log = _log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;

            if (e is AppException)
            {
                //log.LogError(JsonSerializer.Serialize(e as AppException));
            }
            else
            {
                if(e.InnerException!=null)
                {
                    log.LogError($"{e.InnerException.Message}\n{e.InnerException.StackTrace}");
                }
                else
                log.LogError($"{e.Message}\n{e.StackTrace}");
            }


            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;



            AppException appException = null;
            if (e.InnerException is AppException)
            {
                appException = e.InnerException as AppException;
            }
            else
            {
                if (e is AppException)
                {
                    appException = e.InnerException as AppException;
                }
            }
            if (appException != null)
            {
                context.Response.ContentType = "application/json";
                ReturnInfo returnInfo = new ReturnInfo()
                {
                    code = appException.Status,
                    Msg = appException.Message
                };

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
                var result = JsonSerializer.Serialize(returnInfo, options);
                await context.Response.WriteAsync(result).ConfigureAwait(false);
            }
            else
            {
                await context.Response.WriteAsync("应用程序错误，"+e.Message).ConfigureAwait(false);
            }

        }
    }
}
