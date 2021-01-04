using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public class BucketTaggingResponse:StorageResponse
    {
        [JsonPropertyName("Tags")]
        public Tags[] Tags { get; set; }
    }

    public class Tags
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }
}
