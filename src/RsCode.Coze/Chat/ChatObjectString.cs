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
    public class ChatObjectString
    {
        /// <summary>
        /// 多模态消息内容类型，支持设置为：text：文本类型。file：文件类型。image：图片类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 文本内容。在 type 为 text 时必选。
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// 文件或图片内容的 ID。必须是当前账号上传的文件 ID，上传方式可参考上传文件。在 type 为 file 或 image 时，file_id 和 file_url 应至少指定一个。
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// 文件或图片内容的在线地址。必须是可公共访问的有效地址。在 type 为 file 或 image 时，file_id 和 file_url 应至少指定一个。
        /// </summary>
        [JsonPropertyName("file_url")]
        public string FileUrl { get; set; }


    }
}
