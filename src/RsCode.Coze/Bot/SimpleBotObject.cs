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
    public class SimpleBotObject:CozeBaseInfo
    {
        /// <summary>
        /// Bot 的唯一标识。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        /// <summary>
        /// Bot 的名称。
        /// </summary>
        [JsonPropertyName("bot_name")]
        public string BotName { get; set; }

        /// <summary>
        /// Bot 的描述。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Bot 的头像。
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Bot 的最近一次发布时间，格式为 10 位的 Unixtime 时间戳。此 API 返回的 Bot 列表按照此字段降序排列。
        /// </summary>
        [JsonPropertyName("publish_time")]
        public string PublishTime { get; set; }


    }
}
