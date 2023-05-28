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

namespace RsCode.Douyin
{
    public class DouyinResult<T>
        where T:DouyinResponse
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
        public T Data { get; set; }


        [JsonPropertyName("error")]
        public int Error { get; set; }
        [JsonPropertyName("errcode")]
        public long ErrCode { get; set; }
        [JsonPropertyName("errmsg")]
        public string ErrMsg { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
