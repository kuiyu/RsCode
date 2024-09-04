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

using AspectCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using RsCode.AspNetCore.Formatter;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

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
