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
    public class FileObject
    {
        /// <summary>
        /// 已上传的文件 ID。
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// 文件的总字节数。
        /// </summary>
        [JsonPropertyName("bytes")]
        public int Bytes { get; set; }

        /// <summary>
        /// 文件的上传时间，格式为 10 位的 Unixtime 时间戳，单位为秒（s）。
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// 文件名称。
        /// </summary>
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }


    }
}
