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
    /// <summary>
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/log-in/code-2-session"/>
    /// </summary>
    public class Code2SessionRequest:DouyinRequest
    {
        public Code2SessionRequest(DouyinOptions options,string code,string anonymousCode)
        {
            AppId = options.AppId;
            AppSecret = options.Secret;
            if(!string.IsNullOrWhiteSpace(code))
            {
                Code= code; 
            }
            if (!string.IsNullOrWhiteSpace(anonymousCode))
            {
                AnonymousCode= anonymousCode;
            }
        }
        /// <summary>
        /// 小程序 ID
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 小程序的 APP Secret，可以在开发者后台获取
        /// </summary>
        [JsonPropertyName("secret")]
        public string AppSecret { get; set; }

        /// <summary>
        ///获取 access_token 时值为 client_credential
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } 

        /// <summary>
        /// login 接口返回的匿名登录凭证
        /// </summary>
        [JsonPropertyName("anonymous_code")]
        public string AnonymousCode { get; set; }

        public override string GetApiUrl()
        {
            //沙盒地址 https://open-sandbox.douyin.com/api/apps/v2/jscode2session

            return "https://developer.toutiao.com/api/apps/v2/jscode2session";
        }
    }
}
