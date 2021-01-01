/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public class StatResponse:StorageResponse
    {
        
        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [JsonPropertyName("fsize")]
        public long Fsize { set; get; }

        /// <summary>
        /// 文件hash(ETAG)
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { set; get; }

        /// <summary>
        /// 文件MIME类型
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string MimeType { set; get; }

        /// <summary>
        /// 文件上传时间
        /// </summary>
        [JsonPropertyName("putTime")]
        public long PutTime { set; get; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        [JsonPropertyName("type")]
        public int FileType { get; set; }
    }
}
