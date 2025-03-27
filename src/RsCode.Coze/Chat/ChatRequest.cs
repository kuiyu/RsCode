using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.Coze.Chat
{
    public class ChatRequest
    {
        [JsonPropertyName("conversactionId")]
        public string ConversactionId { get; set; }
        [JsonPropertyName("messages")]
        public  ChatMessage[] Messages { get; set; }
        public decimal Temperature { get; set; } = 0.7m;
    }

    public class ChatMesage
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
    }
}
