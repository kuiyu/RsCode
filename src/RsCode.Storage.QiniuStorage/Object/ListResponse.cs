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
    public class ListResponse:StorageResponse
    {
        /// <summary>
        /// marker标记
        /// </summary>
        [JsonPropertyName("marker")]        
        public string Marker { get; set; }

        /// <summary>
        /// 文件列表
        /// </summary>
        [JsonPropertyName("items")]
        public List<ListItem> Items { get; set; }

        /// <summary>
        /// 公共前缀
        /// </summary>
        [JsonPropertyName("commonPrefixes")]
        public List<string> CommonPrefixes { get; set; }
    }

    public class ListItem
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// 文件hash(ETAG)
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [JsonPropertyName("fsize")]
        public long Fsize { get; set; }

        /// <summary>
        /// 文件MIME类型
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonPropertyName("putTime")]
        public long PutTime { get; set; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        [JsonPropertyName("type")]
        public int FileType { get; set; }

        /// <summary>
        /// EndUser字段
        /// </summary>
        [JsonPropertyName("endUser")]
        public string EndUser { get; set; }
    }
}
