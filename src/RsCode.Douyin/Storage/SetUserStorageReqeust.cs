/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.Douyin.Storage
{
    /// <summary>
    /// 以 key-value 形式存储用户数据到小程序平台的云存储服务。若开发者无内部存储服务则可接入，免费且无需申请。一般情况下只存储用户的基本信息，禁止写入大量不相干信息。
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/data-caching/set-user-storage"/>
    /// </summary>
    public class SetUserStorageReqeust:DouyinRequest
    {
        /// <summary>
        /// 服务端 API 调用标识
        /// </summary>
        [JsonPropertyName("access_token")]
        public string SessionKey { get; set; }
        /// <summary>
        /// 登录用户唯一标识
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/log-in/code-2-session/"/>
        /// </summary>
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户登录态签名
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/other/user-login-sign/"/>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
        /// <summary>
        /// 用户登录态签名的编码方法，参考用户登录态签名算法
        /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/other/user-login-sign/"/>
        /// </summary>
        [JsonPropertyName("sig_method")]
        public string SigMethod { get; set; }
        /// <summary>
        /// (body 中) 要设置的用户数据
        /// </summary>
        [JsonPropertyName("kv_list")]
        public Object[] KvList { get; set; }
    }
}
