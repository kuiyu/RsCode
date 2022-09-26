using System.Text.Json.Serialization;

namespace RsCode.Storage.Aliyun
{
    public  class EndPoint
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("sts")]
        public string Sts { get; set; }

        [JsonPropertyName("oss")]
        public string Oss { get; set; }

    }
}
