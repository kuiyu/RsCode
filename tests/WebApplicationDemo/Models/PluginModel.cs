using RsCode.AspNetCore.Plugin;
using System.Text.Json.Serialization;

namespace WebApplicationDemo.Models
{
    public class PluginModel:PluginInfo
    { 
        [JsonPropertyName("status")]
        public int Status { get; set; } = 1;
    }
}
