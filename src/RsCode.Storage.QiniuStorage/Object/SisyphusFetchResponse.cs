using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage.Object
{
    public class SisyphusFetchResponse:StorageResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("wait")]
        public int Wait { get; set; }
    }
}
