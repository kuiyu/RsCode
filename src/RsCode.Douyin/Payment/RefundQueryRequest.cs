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

namespace RsCode.Douyin.Payment
{
    public class RefundQueryRequest:DouyinRequest
    {
        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/ecpay/v1/query_refund";
        }

        /// <summary>
        /// 小程序APPID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [JsonPropertyName("out_refund_no")]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 签名
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
