using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.Coze.Chat
{
    public class ChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = "text";
    }
}
