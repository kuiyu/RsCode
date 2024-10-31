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
    public class MessageCreateRequest
    {
        /// <summary>
        /// 发送这条消息的实体。取值：user：代表该条消息内容是用户发送的。assistant：代表该条消息内容是 Bot 发送的。
        /// </summary>
        [JsonPropertyName("role")]
        public string role { get; set; }

        /// <summary>
        /// 消息的内容，支持纯文本、多模态（文本、图片、文件混合输入）、卡片等多种类型的内容。
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// 消息内容的类型，支持设置为：text：文本object_string：多模态内容，即文本和文件的组合、文本和图片的组合
        /// </summary>
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// 创建消息时的附加消息，获取消息时也会返回此附加消息。自定义键值对，应指定为 Map 对象格式。长度为 16 对键值对，其中键（key）的长度范围为 1～64 个字符，值（value）的长度范围为 1～512 个字符。
        /// </summary>
        [JsonPropertyName("meta_data")]
        public object metaData { get; set; }


    }
}
