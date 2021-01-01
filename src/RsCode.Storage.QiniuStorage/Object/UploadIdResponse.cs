using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
   public class UploadIdResponse:StorageResponse
    {
        [JsonPropertyName("uploadId")]
        public string UploadId { get; set; }
        [JsonPropertyName("expireAt")]
        public long ExpireAt { get; set; }
    }
}
