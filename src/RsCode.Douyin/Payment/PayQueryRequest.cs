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
    /// 支付结果查询 
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/ecpay/pay-list/query"/>
    /// </summary>
    public class PayQueryRequest:DouyinRequest
    {
        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/ecpay/v1/query_order";
        }
        public PayQueryRequest(string appId,string orderNo,string thirdPartyId="")
        {
            AppId = appId;
            OutOrderNo = orderNo;
            ThirdPartyId = thirdPartyId;
        }
        /// <summary>
        /// 小程序APPID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }
        /// <summary>
        /// 开发者侧的订单号, 同一小程序下不可重复
        /// </summary>
        [JsonPropertyName("out_order_no")]
        public string OutOrderNo { get; set; }
        /// <summary>
        ///签名
        /// </summary>
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
        /// <summary>
        /// 第三方平台服务商 id，非服务商模式留空
        /// </summary>
        [JsonPropertyName("thirdparty_id")]
        public string ThirdPartyId { get; set; }
    }
}
