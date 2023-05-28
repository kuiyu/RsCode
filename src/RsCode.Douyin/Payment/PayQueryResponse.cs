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
    public class PayQueryResponse:DouyinResponse
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
        /// <summary>
        /// 开发者侧的订单号
        /// </summary>
        [JsonPropertyName("out_order_no")]
        public string OutOrderNo { get; set; }
        /// <summary>
        /// 抖音侧的订单号
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 支付信息
        /// </summary>
        [JsonPropertyName("payment_info")]
        public PaymentInfo PaymentInfo { get; set; }

        [JsonPropertyName("cps_info")]
        public CpsInfo CpsInfo { get; set; }
    }

    public class PaymentInfo
    {
        /// <summary>
        /// 支付金额，单位为分
        /// </summary>
        [JsonPropertyName("total_fee")]
        public int TotalFee { get; set; }
        /// <summary>
        /// 支付状态枚举值：
        /// SUCCESS：成功 TIMEOUT：超时未支付 PROCESSING：处理中 FAIL：失败
        /// </summary>
        [JsonPropertyName("order_status")]
        public string OrderStatus { get; set; }


        /// <summary>
        /// 支付时间， 格式为"yyyy-MM-dd hh:mm:ss"
        /// </summary>
        [JsonPropertyName("pay_time")]
        public string PayTime { get; set; }
        /// <summary>
        /// 支付渠道， 1-微信支付，2-支付宝支付，10-抖音支付
        /// </summary>
        [JsonPropertyName("way")]
        public int Way { get; set; }


        /// <summary>
        /// 支付渠道侧的支付单号
        /// </summary>
        [JsonPropertyName("channel_no")]
        public string ChannelNo { get; set; }
        /// <summary>
        /// 该笔交易卖家商户号
        /// </summary>
        [JsonPropertyName("seller_uid")]
        public string SellerUid { get; set; }


        /// <summary>
        /// 订单来源视频对应视频 id
        /// </summary>
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }
        /// <summary>
        /// 开发者自定义字段
        /// </summary>
        [JsonPropertyName("cp_extra")]
        public string CpExtra { get; set; }
    }
    public class CpsInfo
    {
        // <summary>
        /// 达人分佣金额，单位为分。后续商户在进行分账时需要注意可分账金额应扣除达人分佣金额。

        //注意：由于订单归因与佣金计算存在延迟，支付成功后立即查询可能未计算完成，建议开发者在支付成功后分账前再进行查询。
        /// </summary>
        [JsonPropertyName("share_amount")]
        public string ShareAmount { get; set; }
        // <summary>
        /// 达人抖音号
        /// </summary>
        [JsonPropertyName("douyin_id")]
        public string DouyinId { get; set; }
        // <summary>
        /// 达人昵称
        /// </summary>
        [JsonPropertyName("nickname")]
        public string NickName { get; set; }

    }
}
