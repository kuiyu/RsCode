using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public class UploadPartResponse:StorageResponse
    {
        /// <summary>
        /// 上传块内容的 etag ，用来标识块，completeMultipartUpload API 调用的时候作为参数进行文件合成
        /// </summary>
        [JsonPropertyName("etag")]
        public string ETag { get; set; }
        /// <summary>
        /// 上传块内容的 md5
        /// </summary>
        [JsonPropertyName("md5")]
        public string Md5 { get; set; }
    }
}
