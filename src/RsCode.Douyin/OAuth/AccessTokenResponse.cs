/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Newtonsoft.Json;
using RsCode.Douyin.Core;
using System.Text.Json.Serialization;
namespace RsCode.Douyin.OAuth
{
    //https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/interface-request-credential/get-access-token
    public class AccessTokenResponse:DouyinResponse
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
        public AccessTokenResult Data { get; set; }

        
      
    }

    public class AccessTokenResult
    {
        /// <summary>
        ///获取的 access_token
        /// </summary>
        [JsonPropertyName("access_token")]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// access_token 有效时间，单位：秒
        /// </summary>
        [JsonPropertyName("expires_in")]
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
