/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using System.Text.Json.Serialization;

namespace RsCode.Douyin.OAuth
{
    //https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/interface-request-credential/get-access-token
    public class AccessTokenRequest: DouyinRequest
    {
        public AccessTokenRequest()
        {
            
        }
        public AccessTokenRequest(string appid,string secret)
        {
            AppId = appid;
            AppSecret = secret;
        }
        public AccessTokenRequest(DouyinOptions options)
        {
            AppId=options.AppId; AppSecret=options.Secret;
        }
        /// <summary>
        /// 获取或设置应用 ID
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 获取或设置应用密钥。
        /// </summary>
        [JsonPropertyName("secret")]
        public string AppSecret { get; set; }

        /// <summary>
        ///获取 access_token 时值为 client_credential
        /// </summary>
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "client_credential";

     

        public override string GetApiUrl()
        {
            //https://open-sandbox.douyin.com/api/apps/v2/token
            return "https://developer.toutiao.com/api/apps/v2/token";
        }
    }
}
