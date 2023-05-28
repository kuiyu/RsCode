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
    /// 预支付
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/ecpay/pay-list/pay"/>
    /// </summary>
    public class PrepayRequest:DouyinRequest
    {
        public PrepayRequest(string appId, string orderNo,decimal totalAmount,string notifyUrl,string subject="抖音商品")
        {
            AppId =appId;
            OutOrderNo= orderNo;
            TotalAmount = DouyinTool.Price(totalAmount);
            NotifyUrl = notifyUrl;
            Subject = subject;
            Body = subject;
        }

        public override string GetApiUrl()
        {
            string url = "https://developer.toutiao.com/api/apps/ecpay/v1/create_order";
            return url;
        }

        /// <summary>
        /// 小程序APPID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }
        /// <summary>
        /// 开发者侧的订单号。 只能是数字、大小写字母_-*且在同一个app_id下唯一
        /// </summary>
        [JsonPropertyName("out_order_no")]
        public string OutOrderNo { get; set; }
        /// <summary>
        /// 支付价格。 单位为[分]
        /// </summary>
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
        /// <summary>
        /// 商品描述。 长度限制不超过 128 字节且不超过 42 字符
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        /// <summary>
        /// 商品详情 长度限制不超过 128 字节且不超过 42 字符
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }
        /// <summary>
        /// 订单过期时间(秒)。最小5分钟，最大2天，小于5分钟会被置为5分钟，大于2天会被置为2天
        /// </summary>
        [JsonPropertyName("valid_time")]
        public int ValidTime { get; set; } = 3600 * 48;
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
        /// 商户自定义回调地址，必须以 https 开头，支持 443 端口。 指定时，支付成功后抖音会请求该地址通知开发者
        /// </summary>
        [JsonPropertyName("notify_url")]
        public string NotifyUrl { get; set; }
        /// <summary>
        ///   第三方平台服务商 id，非服务商模式留空
                /// </summary>
                [JsonPropertyName("thirdparty_id")]
        public string ThirdPartyId { get; set; }
        /// <summary>
        /// 可用此字段指定本单使用的收款商户号（目前为灰度功能，需要联系平台运营添加白名单，白名单添加1小时后生效；未在白名单的小程序，该字段不生效）
        /// </summary>
        [JsonPropertyName("store_uid")]
        public string StoreUid { get; set; }
        /// <summary>
        /// 是否屏蔽支付完成后推送用户抖音消息，1-屏蔽 0-非屏蔽，默认为0。 特别注意： 若接入POI, 请传1。因为POI订单体系会发消息，所以不用再接收一次担保支付推送消息，
        /// </summary>
        [JsonPropertyName("disable_msg")]
        public int DisableMsg { get; set; }
        /// <summary>
        /// 支付完成后推送给用户的抖音消息跳转页面，开发者需要传入在app.json中定义的链接，如果不传则跳转首页。
        /// </summary>
        [JsonPropertyName("msg_page")]
        public string MsgPage { get; set; }
        /// <summary>
        /// 订单拓展信息，详见下面
        /// expand_order_info参数说明
        /// </summary>
        [JsonPropertyName("expand_order_info")]
        public ExpandOrderInfo ExpandOrderInfo { get; set; }
        /// <summary>
        /// 屏蔽指定支付方式，屏蔽多个支付方式，请使用逗号","分割，枚举值：
        /// 屏蔽微信支付：LIMIT_WX
        /// 屏蔽支付宝支付：LIMIT_ALI
        /// 屏蔽抖音支付：LIMIT_DYZF
        /// 特殊说明：若之前开通了白名单，平台会保留之前屏蔽逻辑；若传入该参数，会优先以传入的为准，白名单则无效
         /// </summary>
        [JsonPropertyName("limit_pay_way")]
        public string LimitPayWay { get; set; }
      
    }

    public class ExpandOrderInfo
    {
        /// <summary>
        /// 配送费原价，单位为[分]，仅外卖小程序需要传对应信息
        /// </summary>
        [JsonPropertyName("original_delivery_fee")]
        public int OriginalDeliveryFee { get; set; }
        /// <summary>
        /// 实付配送费，单位为[分]，仅外卖小程序需要传对应信息
        /// </summary>
        [JsonPropertyName("actual_delivery_fee")]
        public int ActualDeliveryFee { get; set; }
    }
}
