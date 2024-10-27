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
    public class SourceInfoObject
    {
        /// <summary>
        /// 本地文件的 Base64 编码。
        /// </summary>
        [JsonPropertyName("file_base64")]
        public string FileBase64 { get; set; }

        /// <summary>
        /// 本地文件格式，即文件后缀，例如 txt。格式支持 pdf、txt、doc、docx 类型。上传的文件类型应与知识库类型匹配，例如 txt 文件只能上传到文档类型的知识库中。
        /// </summary>
        [JsonPropertyName("file_type")]
        public string FileType { get; set; }

        /// <summary>
        /// 网页的 URL 地址。
        /// </summary>
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; }

        /// <summary>
        /// 文件的上传方式。支持设置为 1，表示上传在线网页。
        /// </summary>
        [JsonPropertyName("document_source")]
        public int DocumentSource { get; set; }

    }
}
