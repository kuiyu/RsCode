using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 从uc.qbox.me返回的消息
    /// </summary>
    internal class ZoneInfo
    {
        [JsonPropertyName("ttl")]
        public int Ttl { get; set; }
        [JsonPropertyName("io")] public Io Io { set; get; } 
        [JsonPropertyName("up")] public Up Up { set; get; }
        [JsonPropertyName("uc")] public Uc Uc { set; get; }
        [JsonPropertyName("rs")] public Rs Rs { get; set; }

        [JsonPropertyName("rsf")] public Rsf Rsf { get; set; }

        [JsonPropertyName("api")] public Api Api { get; set; }
    }

    internal class Io
    {
        [JsonPropertyName("src")]
        public Src Src { set; get; }
    }

    internal class Src
    {
        [JsonPropertyName("main")]
        public string[] Main { set; get; }
    }
    internal class Uc
    {
        [JsonPropertyName("acc")]
        public Acc Acc { get; set; }
    }

    internal class Up
    {
        [JsonPropertyName("acc")]
        public UpDomain Acc { set; get; }
        [JsonPropertyName("old_acc")]
        public UpDomain OldAcc { set; get; }
        [JsonPropertyName("src")]
        public UpDomain Src { set; get; }
        [JsonPropertyName("old_src")]
        public UpDomain OldSrc { set; get; }
    }

    internal class Rs
    {
        [JsonPropertyName("acc")]
        public UpDomain Acc { get; set; }
    }

    internal class Rsf
    {
        [JsonPropertyName("acc")]
        public UpDomain Acc { get; set; }
    }

    internal class Api
    {
        [JsonPropertyName("acc")]
        public UpDomain Acc { get; set; }
    }
    internal class UpDomain
    {
        [JsonPropertyName("main")]
        public string[] Main { set; get; }
        [JsonPropertyName("backup")]
        public string[] Backup { set; get; }
        [JsonPropertyName("info")]
        public string Info { set; get; }
    }

    internal class Acc
    {
        [JsonPropertyName("main")]
        public string[] Main { get; set; }
    }
     
}
