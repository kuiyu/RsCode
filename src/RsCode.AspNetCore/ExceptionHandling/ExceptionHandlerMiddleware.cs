﻿/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Microsoft.AspNetCore.Http;
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
        private readonly RequestDelegate _next;
       
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;
            Exception exception = e;

            if (e is AppException)
            {

            }else
            {
                if (e.InnerException != null && e.InnerException is AppException)
                {
                    exception = e.InnerException;
                }
            }
            
            bool isAjax = IsAjax(context.Request);
            
            var api = context.Request.Path.StartsWithSegments("/api");
            if (api || isAjax)
            {
                await WriteExceptionAsync(context, exception).ConfigureAwait(false);
            }
            else
            {
                int statusCode = 500;
                var msg = exception.Message;
                if(exception is AppException)
                {
                    statusCode = 0;
                    msg = ((RsCode.AspNetCore.AppException)exception).Message;
                }
                
                context.Response.Clear();
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync($"Error ,{msg}");
            }
        }


        private async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            int statusCode = 500;
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is AppException)
            {
                context.Response.StatusCode = 200; statusCode = 200;
            }

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

            if (e is AppException appException)
            {
                context.Response.ContentType = "application/json";
                ReturnInfo returnInfo = new ReturnInfo()
                {
                    code = appException.Status,
                    Msg = appException.Message
                };
                context.Response.StatusCode = 0;
                var result = JsonSerializer.Serialize(returnInfo, options);
                await context.Response.WriteAsync(result).ConfigureAwait(false);
            }
            else
            {
                 
                context.Response.ContentType = "application/json";
                ReturnInfo returnInfo = new ReturnInfo()
                {
                    code = statusCode,
                    Msg = e.Message
                };

                var result = JsonSerializer.Serialize(returnInfo, options);
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result).ConfigureAwait(false);
            }
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
