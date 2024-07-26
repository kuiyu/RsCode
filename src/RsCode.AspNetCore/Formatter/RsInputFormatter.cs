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
using AspectCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RsCode.AspNetCore.Formatter;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    public class RsInputFormatter : InputFormatter
    {
         
        public RsInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json")); 
        }
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            string s = "";
            var contextType = context.HttpContext.Request.ContentType;
            
            try
            {
                var request = context.HttpContext.Request;
                var type = context.ModelType;
              
                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    s = await reader.ReadToEndAsync();
                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                    jsonSerializerOptions.Converters.Add( new DateTimeConverter());
                    jsonSerializerOptions.Converters.Add(new BoolConverter());
                    jsonSerializerOptions.Converters.Add(new IntConverter());
                    jsonSerializerOptions.Converters.Add(new Int64Converter());
                    jsonSerializerOptions.Converters.Add(new DecimalConverter());
                    jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    jsonSerializerOptions.IgnoreNullValues = true;
                    var obj = JsonSerializer.Deserialize(s, type,jsonSerializerOptions);
                    return await InputFormatterResult.SuccessAsync(obj);
                }
            }
            catch (Exception ex)
            {
                var logger = context.HttpContext.RequestServices.Resolve<ILogger<RsInputFormatter>>();

                logger?.LogError("传参" + s + " 转换错误" + ex.Message);
                return await InputFormatterResult.FailureAsync();
            }

        }
    }
}
