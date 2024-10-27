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
    public class PluginApiOjbect
    {
        /// <summary>
        /// 工具的唯一标识。
        /// </summary>
        [JsonPropertyName("api_id")]
        public string ApiId { get; set; }

        /// <summary>
        /// 工具的名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 工具的描述。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }


    }
}
