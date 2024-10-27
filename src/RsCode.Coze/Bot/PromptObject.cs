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
    public class PromptObject:CozeBaseInfo
    {
        /// <summary>
        /// Bot 配置的提示词。
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }


    }
}
