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
    /// <summary>
    /// https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/ecpay/order/order-sync
    /// </summary>
    public class OrderSyncResponse:DouyinResponse
    {
        /// <summary>
        /// 返回码，详见错误码
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrMsg{ get; set; }
        /// <summary>
        /// POI 等关联业务推送结果，非 POI 订单为空，JSON 字符串
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
