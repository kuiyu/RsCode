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

using System.Text.Json.Nodes;
using System.Text.Json;

namespace RsCode.AspNetCore
{
    public class WebHelper
    {
        IHttpContextAccessor HttpContextAccessor { get; set; }
        public WebHelper(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public async Task<T> ReadRequestAsync<T>() where T : class
        {
            try
            {
                var bodyStream = HttpContextAccessor?.HttpContext?.Request.Body;
                if (bodyStream == null)
                {
                    return null;
                }
                using var reader = new StreamReader(bodyStream);
                var requestBody = await reader.ReadToEndAsync();

                var jsonOption = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                var request = JsonSerializer.Deserialize<T>(requestBody, jsonOption);
                var json = JsonObject.Parse(requestBody);
                //request.Test = json["Test"] as string;

                return request;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
