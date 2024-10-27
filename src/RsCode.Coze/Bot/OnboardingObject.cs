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
    public class OnboardingObject
    {
        /// <summary>
        /// Bot 配置的开场白内容。
        /// </summary>
        [JsonPropertyName("prologue")]
        public string Prologue { get; set; }

        /// <summary>
        /// Bot 配置的推荐问题列表。未开启用户问题建议时，不返回此字段。
        /// </summary>
        [JsonPropertyName("suggested_questions")]
        public string[] SuggestedQuestions { get; set; }



    }
}
