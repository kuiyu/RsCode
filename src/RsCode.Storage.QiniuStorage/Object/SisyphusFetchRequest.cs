using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage.Object
{
   public class SisyphusFetchRequest:QiniuStorageRequest
    {
        /// <summary>
        /// 需要抓取的url,支持设置多个用于高可用,以';'分隔,当指定多个url时可以在前一个url抓取失败时重试下一个
        /// </summary>
        [JsonPropertyName("url")]
        public string URL { get; set; }
        /// <summary>
        /// 所在区域的bucket
        /// </summary>
        [JsonPropertyName("bucket")]
        public string Bucket { get; set; }
        /// <summary>
        /// 从指定url下载数据时使用的Host
        /// </summary>
        [JsonPropertyName("host")]
        public string Host { get { return new Uri(URL).Host; } }

        /// <summary>
        /// 文件存储的key,不传则使用文件hash作为key
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        ///文件md5,传入以后会在存入存储时对文件做校验，校验失败则不存入指定空间
        /// </summary>
        [JsonPropertyName("md5")]
        public string Md5 { get; set; }
        /// <summary>
        /// 文件etag,传入以后会在存入存储时对文件做校验，校验失败则不存入指定空间
        /// </summary>
        [JsonPropertyName("etag")]
        public string Etag { get; set; }
        /// <summary>
        /// 回调URL，详细解释请参考上传策略中的callbackUrl
        /// </summary>
        [JsonPropertyName("callbackurl")]
        public string CallbackUrl { get; set; }
        /// <summary>
        /// 回调Body，如果callbackurl不为空则必须指定。
        /// 上传成功后，七牛云向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。
        /// 业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量、但不支持自定义变量。
        /// callbackBody 要求是合法的 url query string。
        /// 例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。
        /// </summary>
        [JsonPropertyName("callbackbody")]
        public string CallbackBody { get; set; }
        /// <summary>
        /// 回调Body内容类型,默认为"application/x-www-form-urlencoded"
        /// </summary>
        [JsonPropertyName("callbackbodytype")]
        public string CallbackBodyType { get; set; }
        /// <summary>
        /// 回调时使用的Host
        /// </summary>
        [JsonPropertyName("callbackhost")]
        public string CallbackHost { get; set; }
        /// <summary>
        /// 存储文件类型 0:标准存储(默认),1:低频存储,2:归档存储
        /// </summary>
        [JsonPropertyName("file_type")]
        public int FileType { get; set; }
        /// <summary>
        /// 如果空间中已经存在同名文件则放弃本次抓取(仅对比Key，不校验文件内容)
        /// </summary>
        [JsonPropertyName("ignore_same_key")]
        public bool IgnoreSameKey { get; set; }
        
        
         




        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/sisyphus/fetch";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
