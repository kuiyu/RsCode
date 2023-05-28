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
    public class RefundQueryResponse:DouyinResponse
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

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("refundInfo")]
        public RefundResult RefundInfo { get; set; }

    }

    public class RefundResult
    {
        /// <summary>
        /// 退款金额，单位为分
        /// </summary>
        [JsonPropertyName("refund_amount")]
        public int RefundAmount { get; set; }
        /// <summary>
        /// 退款状态枚举值：
        /// SUCCESS：成功  PROCESSING：处理中 FAIL：失败
        /// </summary>
        [JsonPropertyName("refund_status")]
        public string RefundStatus { get; set; }


        /// <summary>
        ///退款时间， 格式为"yyyy-MM-dd hh:mm:ss"
        /// </summary>
        [JsonPropertyName("refunded_at")]
        public string RefundAt { get; set; }
        /// <summary>
        /// 退款渠道，TRUE：分账后退款，现金户出款
        /// FALSE：分账前退款，在途户出款
                /// </summary>
                [JsonPropertyName("is_all_settled")]
        public bool IsAllSettled { get; set; }

        /// <summary>
        /// 抖音退款单号
        /// </summary>
        [JsonPropertyName("refund_no")]
        public string RefundNo { get; set; }
        /// <summary>
        /// 开发者自定义字段，回调原样回传
        /// </summary>
        [JsonPropertyName("cp_extra")]
        public string CpExtra { get; set; }
        /// <summary>
        /// 退款错误描述
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
    }

}
