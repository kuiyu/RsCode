using AspectCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            try
            {
                var request = context.HttpContext.Request;
                var type = context.ModelType;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    s = await reader.ReadToEndAsync();
                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                    jsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    var obj = JsonSerializer.Deserialize(s, type,jsonSerializerOptions);
                    return await InputFormatterResult.SuccessAsync(obj);
                }
            }
            catch (Exception ex)
            {
                var logger = context.HttpContext.RequestServices.Resolve<ILogger<RsInputFormatter>>();
                logger.LogDebug("传参" + s + " 转换错误" + ex.Message);
                return await InputFormatterResult.FailureAsync();
            }

        }
    }
}
