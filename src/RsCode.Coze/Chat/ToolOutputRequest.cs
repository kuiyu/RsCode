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
    public class ToolOutputRequest
    {
        /// <summary>
        /// 工具执行结果。详细说明可参考 ToolOutput Object。
        /// </summary>
        [JsonPropertyName("tool_outputs")]
        public ToolOutputObject[] ToolOutputs { get; set; }

        /// <summary>
        /// 是否开启流式响应。true：填充之前对话中的上下文，继续流式响应。false：（默认）非流式响应，仅回复对话的基本信息。
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }


    }
}
