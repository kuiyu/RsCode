/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
   gitee
   https://gitee.com/rswl/RsCode.git
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// webapi格式化响应数据
    /// </summary>
    public class RsOutputFormatter : TextOutputFormatter
    {
        string _dateFormat;
        bool _CameCasePropertyName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateFormat">默认格式 yyyy-Mm-dd HH:mm:ss</param>
        /// <param name="CameCasePropertyName">是否使用驼峰命名规则,默认false</param>
        public RsOutputFormatter(string DateFormat="yyyy-MM-dd HH:mm:ss",bool CameCasePropertyName=false)
        {
            _dateFormat = DateFormat;
            _CameCasePropertyName = CameCasePropertyName;
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        //指定序列化的类型
        protected override bool CanWriteType(Type type)
        {
            if(type == typeof(AppException))
            {
                return true;
            }else
            {
                return false;
            }
           
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            
                var resultInfo = new ReturnInfo()
                {
                    Success = true,
                    code = response.StatusCode,
                    Msg = "操作成功",
                    Result = context.Object
                };
                if (context.Object == null)
                {
                    resultInfo.Success = false;
                    resultInfo.Result = null;
                }

                if (context.ObjectType == typeof(ReturnInfo))
                {
                    resultInfo = context.Object as ReturnInfo;
                }

                 if (context.ObjectType == typeof(AppException))
                {
                    resultInfo.Success = false;
                    resultInfo.Msg = context.Object == null ? "" : context.Object as string;
                    resultInfo.Result = null;
                }

                var options = new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    IgnoreNullValues = true,
                    WriteIndented = true,
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = _CameCasePropertyName,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase //小写开头
                };
                options.WriteIndented = true;

                options.Converters.Add(new DateTimeConverter(_dateFormat));
                string s = JsonSerializer.Serialize(resultInfo, options);
                await response.WriteAsync(s, Encoding.UTF8);
           
        }
    }

    class DataTimeJsonConverter:JsonConverter<DateTime>
    {
        string formater;
        public DataTimeJsonConverter(string dateFormater= "yyyy-MM-dd HH:mm:ss")
        {
            formater = dateFormater;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formater));
        }

    }
}
