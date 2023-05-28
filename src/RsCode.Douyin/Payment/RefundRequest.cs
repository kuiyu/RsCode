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
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/ecpay/refund-list/refund"/>
    /// </summary>
    public class RefundRequest:DouyinRequest
    {
        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/ecpay/v1/create_refund";
        }
        /// <summary>
        /// 小程序APPID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }
        /// <summary>
        /// 商户分配支付单号，标识进行退款的订单
        /// </summary>
        [JsonPropertyName("out_order_no")]
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 商户分配退款号，保证在商户中唯一
        /// </summary>
        [JsonPropertyName("out_refund_no")]
        public string OutRefundNo { get; set; }
        /// <summary>
        ///退款原因
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        /// <summary>
        /// 退款金额，单位分
        /// </summary>
        [JsonPropertyName("refund_amount")]
        public int RefundAmount { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
        /// <summary>
        /// 开发者自定义字段，回调原样回传。 超过最大长度会被截断
        /// </summary>
        [JsonPropertyName("cp_extra")]
        public string CpExtra { get; set; }
        /// <summary>
        /// 商户自定义回调地址，必须以 https 开头，支持 443 端口
        /// </summary>
        [JsonPropertyName("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 第三方平台服务商id，服务商模式接入必传，非服务商模式留空
        /// </summary>
        [JsonPropertyName("thirdparty_id")]
        public string ThirdPartyId { get; set; }
        /// <summary>
        /// 是否屏蔽支付完成后推送用户抖音消息，1-屏蔽 0-非屏蔽，默认为0。 特别注意： 若接入POI, 请传1。因为POI订单体系会发消息，所以不用再接收一次担保支付推送消息，请传1
        /// </summary>
        [JsonPropertyName("disable_msg")]
        public int DisableMsg { get; set; }
        /// <summary>
        /// 退款完成后推送给用户的抖音消息跳转页面，开发者需要传入在app.json中定义的链接，如果不传则跳转首页。
        /// </summary>
        [JsonPropertyName("msg_page")]
        public string MsgPage { get; set; }
     
    }
}
