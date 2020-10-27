using System.Text.Json.Serialization;

namespace RsCode.Storage
{
    public class UploadResult
    {
        [JsonPropertyName("res")]
        public string Res { get; set; } = "";
        [JsonPropertyName("key")]
        public string Key { get; set; } = "";
    }
}
