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
    public class PluginObject
    {
        /// <summary>
        /// 插件唯一标识。
        /// </summary>
        [JsonPropertyName("plugin_id")]
        public string PluginId { get; set; }

        /// <summary>
        /// 插件名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 插件描述。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// 插件头像。
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 插件的工具列表信息，详情参考 PluginAPI object。
        /// </summary>
        [JsonPropertyName("api_info_list")]
        public object[] ApiInfoList { get; set; }


    }
}
