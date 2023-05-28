/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Douyin.Core;
using System.Text.Json.Serialization;

namespace RsCode.Douyin.OAuth
{
    public class Code2SessionResponse:DouyinResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonPropertyName("err_no")]
        public long ErrNo { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonPropertyName("err_tips")]
        public string ErrTips { get; set; }

        [JsonPropertyName("data")]
        public Code2SessionResult Data { get; set; }
       
    }

    public class Code2SessionResult
    {
        /// <summary>
        /// 会话密钥，如果请求时有 code 参数才会返回
        /// </summary>
        [JsonPropertyName("session_key")]
        public string SessionKey { get; set; }
        /// <summary>
        /// 用户在当前小程序的 ID，如果请求时有 code 参数才会返回
        /// </summary>
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 匿名用户在当前小程序的 ID，如果请求时有 anonymous_code 参数才会返回
        /// </summary>
        [JsonPropertyName("anonymous_openid")]
        public string AnonymousOpenId { get; set; }
        /// <summary>
        /// 用户在小程序平台的唯一标识符，请求时有 code 参数才会返回。如果开发者拥有多个小程序，可通过 unionid 来区分用户的唯一性。
        /// </summary>
        [JsonPropertyName("unionid")]
        public string UnionId { get; set; }
    }
}
