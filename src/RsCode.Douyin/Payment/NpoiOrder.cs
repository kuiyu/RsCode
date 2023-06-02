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
    /// 非抖音商品库的商品称为 POI 订单
    /// 需使用真机调试生成的openid做订单同步
    /// </summary>
    public class NpoiOrder
    {
        /// <summary>
        /// 开发者侧业务单号。用作幂等控制。该订单号是和担保支付的支付单号绑定的，也就是预下单时传入的 out_order_no 字段，长度 <= 64byte
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 订单创建的时间，13 位毫秒时间戳
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }
        /// <summary>
        ///订单状态
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        /// <summary>
        /// 订单商品总数
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// 订单总价，单位为分
        /// </summary>
        [JsonPropertyName("total_price")]
        public int TotalPrice { get; set; }
        /// <summary>
        /// 小程序订单详情页 path，长度<=1024 byte
        /// </summary>
        [JsonPropertyName("detail_url")]
        public string DetailUrl { get; set; }
        /// <summary>
        /// 子订单商品列表，不可为空
        /// </summary>
        [JsonPropertyName("item_list")]
        public NpoiItem[] ItemList { get; set; }

    }

    public class NpoiItem
    {
        /// <summary>
        /// 开发者侧商品 ID，长度 <= 64 byte
        /// </summary>
        [JsonPropertyName("item_code")]
        public string ItemCode { get; set; }
        /// <summary>
        /// 子订单商品图片 URL， 长度 <= 512 byte
        /// </summary>
        [JsonPropertyName("img")]
        public string Img { get; set; }
        /// <summary>
        /// 子订单商品介绍标题，长度 <= 256 byte
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
        /// <summary>
        /// 子订单商品介绍副标题，长度 <= 256 byte
        /// </summary>
        [JsonPropertyName("sub_title")]
        public string SubTitle { get; set; }
        /// <summary>
        /// 单类商品的数目
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// 单类商品的总价，单位为分
        /// </summary>
        [JsonPropertyName("price")]
        public int Price { get; set; }
    }
}
