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
    public class RefundResponse:DouyinResponse
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
        /// 担保交易服务端退款单号
        /// </summary>
        [JsonPropertyName("refund_no")]
        public string RefundNo { get; set; }
    }
}
