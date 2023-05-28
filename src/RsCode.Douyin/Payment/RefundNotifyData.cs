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
    public class RefundNotifyData
    {
        /// <summary>
        /// Unix 时间戳，字符串类型
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        /// <summary>
        /// 订单信息的 json 字符串
        /// 对应RefundNotifyResult
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [JsonPropertyName("msg_signature")]
        public string MsgSignature { get; set; }
        /// <summary>
        /// 回调类型标记，支付成功回调为"payment"
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
