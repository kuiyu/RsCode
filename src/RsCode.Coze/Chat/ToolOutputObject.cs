/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class ToolOutputObject
    {
        /// <summary>
        /// 上报运行结果的 ID。你可以在发起对话（V3）接口响应的 tool_calls 字段下查看此 ID。
        /// </summary>
        [JsonPropertyName("tool_call_id")]
        public string ToolCallId { get; set; }

        /// <summary>
        /// 工具的执行结果。
        /// </summary>
        [JsonPropertyName("output")]
        public string Output { get; set; }


    }
}
