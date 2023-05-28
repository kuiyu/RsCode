/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Douyin.Payment
{
    public class PrepayResponse:DouyinResponse
    {
        /// <summary>
        /// 返回码，详见错误码
        /// </summary>
        [JsonPropertyName("err_no")]
        public int ErrNo { get; set; }
        /// <summary>
        /// 返回码描述，详见错误码描述
        /// </summary>
        [JsonPropertyName("err_tips")]
        public string ErrTips { get; set; }

        [JsonPropertyName("data")]
        public PrepayResult Data { get; set; }
       
    }

    public class PrepayResult
    {
        /// <summary>
        /// 抖音侧唯一订单号
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 签名后的订单信息
        /// </summary>
        [JsonPropertyName("order_token")]
        public string OrderToken { get; set; }
    }
}
