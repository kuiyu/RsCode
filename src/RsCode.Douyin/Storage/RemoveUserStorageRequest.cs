/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Douyin.Storage
{
    /// <summary>
    /// 删除存储到字节跳动的云存储服务的 key-value 数据。当开发者不需要该用户信息时，需要删除，以免占用过大的存储空间。
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/data-caching/remove-user-storage"/>
    /// </summary>
    public class RemoveUserStorageRequest: DouyinRequest
    {
        /// <summary>
        /// 服务端 API 调用标识
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/interface-request-credential/get-access-token/"/>
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 登录用户唯一标识
        /// </summary>
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户登录态签名，参考用户登录态签名算法
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/other/user-login-sign/"/>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
        /// <summary>
        /// 用户登录态签名的编码方法
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/other/user-login-sign/"/>
        /// </summary>
        [JsonPropertyName("sig_method")]
        public string SigMethod { get; set; }

        /// <summary>
        /// (body 中) 要删除的用户数据的 key list
        /// </summary>
        [JsonPropertyName("key")]
        public string[] Key { get; set; }

        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/remove_user_storage";
        }
    }
}
