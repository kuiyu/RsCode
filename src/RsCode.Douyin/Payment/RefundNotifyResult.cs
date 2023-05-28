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
    public class RefundNotifyResult
    {
        /// <summary>
        /// 当前交易发起的小程序id
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }
        /// <summary>
        ///开发者侧的退款订单号
        /// </summary>
        [JsonPropertyName("cp_refundno")]
        public string CpRefundNo { get; set; }
        /// <summary>
        /// 预下单时开发者传入字段
        /// </summary>
        [JsonPropertyName("cp_extra")]
        public string CpExtra { get; set; }
        /// <summary>
        /// 状态枚举值：
        /// SUCCESS：成功   FAIL：失败
        /// </summary>
        [JsonPropertyName("refund_status")]
        public string RefundStatus { get; set; }

        /// <summary>
        /// 退款金额，单位为分
        /// </summary>
        [JsonPropertyName("refund_amount")]
        public int RefundAmount { get; set; }
        /// <summary>
        ///退款时间， 格式为"yyyy-MM-dd hh:mm:ss"
        /// </summary>
        [JsonPropertyName("refunded_at")]
        public string RefundAt { get; set; }
        /// <summary>
        /// 退款失败原因描述，详见发起退款错误码
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
        /// <summary>
        /// 抖音侧订单号
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 抖音退款单号
        /// </summary>
        [JsonPropertyName("refund_no")]
        public string RefundNo { get; set; }
        /// <summary>
        /// 是否为分账后退款
        /// </summary>
        [JsonPropertyName("is_all_settled")]
        public bool IsAllSettled { get; set; }
    }
}
