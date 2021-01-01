using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
 public   class UploadCompleteResponse:StorageResponse
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        /// 目标资源的 hash 值，可用于 Etag 头部
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}
