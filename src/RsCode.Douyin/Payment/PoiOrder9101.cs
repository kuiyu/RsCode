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
    /// 9101团购券类型
    /// https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/ecpay/order/order-sync#_接口说明
    /// </summary>
    public class PoiOrder9101
    {
        /// <summary>
        /// 开发者侧业务单号。用作幂等控制。该订单号是和担保支付的支付单号绑定的，也就是预下单时传入的 out_order_no 字段，长度 <= 64byte
        /// </summary>
        [JsonPropertyName("ext_order_id")]
        public string ExtOrderId { get; set; }
        /// <summary>
        ///状态
        ///枚举值：

///10：已取消（抖音订单中心可看到，状态为"已取消"）
///110：待支付
///310：未使用
///340：已使用
///410：退款中
///420： 退款成功
///430： 退款失败
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// 商铺名字，长度 <= 256 byte
        /// </summary>
        [JsonPropertyName("shop_name")]
        public string ShopName { get; set; }
        /// <summary>
        ///订单详情页的外链跳转类型，通过该接口上传的都为 2

        ///1：H5
        ///2：抖音小程序
        /// </summary>
        [JsonPropertyName("entry_type")]
        public int EntryType { get; set; }
        /// <summary>
        /// 订单详情页的外链跳转 schema 参数，格式为 json 字符串。长度 <= 512byte，具体参数详见 entry_schema 说明
        /// </summary>
        [JsonPropertyName("entry_schema")]
        public string EntrySchema { get; set; }
        /// <summary>
        /// 下单时间（13位毫秒时间戳）
        /// </summary>
        [JsonPropertyName("create_order_time")]
        public int CreateOrderTime { get; set; }
        /// <summary>
        /// 订单描述，长度<=500 byte
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
        /// <summary>
        /// 订单总金额（单位：分）
        /// </summary>
        [JsonPropertyName("total_price")]
        public int TotalPrice { get; set; }
        /// <summary>
        /// 支付时间（13位毫秒时间戳），未付款时不用传
        /// </summary>
        [JsonPropertyName("pay_time")]
        public string PayTime { get; set; }
        /// <summary>
        /// 开发者侧卡劵核销门店ID，未核销时不用传，长度 <= 256 byte
        /// </summary>
        [JsonPropertyName("ext_valid_shop_id")]
        public string ExtValidShopId { get; set; }

        /// <summary>
        ///开发者侧卡劵核销门店对应的抖音poiId，ext_valid_shop_id未匹配抖音POI时不用传，长度<= 128 byte
        /// </summary>
        [JsonPropertyName("valid_poi_id_str")]
        public string ValidPoiIdStr { get; set; }
        /// <summary>
        /// 订单总价，单位为分
        /// </summary>
        [JsonPropertyName("ext_goods_id")]
        public string ExtGoodsId { get; set; }
        /// <summary>
        /// 商品名称，长度 <= 256 byte
        /// </summary>
        [JsonPropertyName("goods_name")]
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品描述信息。向用户介绍商品，长度 <= 120byte
        /// </summary>
        [JsonPropertyName("goods_info")]
        public string GoodsInfo { get; set; }

        /// <summary>
        /// 商品图片，完整的url地址 长度 <= 512 byte
       /// </summary>
        [JsonPropertyName("goods_cover_image")]
        public string GoodsCoverImage { get; set; }
        /// <summary>
        /// 商品详情页的外链跳转类型,通过该接口上传的都为2     1: H5 2: 抖音小程序
        /// </summary>
        [JsonPropertyName("goods_entry_type")]
        public int GoodsEntryType { get; set; }
        /// <summary>
        /// 商品详情页的外链跳转schema参数，格式为 JSON 字符串，长度 <= 512 byte， 详见 entry_schema 说明
        /// </summary>
        [JsonPropertyName("goods_entry_schema")]
        public string GoodsEntrySchema { get; set; }
        /// <summary>
        /// 生效时间，yyyy-MM-dd HH:mm:ss 格式字符串，24 小时制
        /// </summary>
        [JsonPropertyName("start_valid_time")]
        public string StartValidTime { get; set; }
        /// <summary>
        /// 失效时间，yyyy-MM-dd HH:mm:ss格式字符串，24小时制
        /// </summary>
        [JsonPropertyName("end_valid_time")]
        public string EndValidTime { get; set; }
        /// <summary>
        /// 用户购买团购券的数量
        /// </summary>
        [JsonPropertyName("ticket_num")]
        public int TicketNum { get; set; }
        /// <summary>
        /// 开发者侧券 ID，该信息用于用户可以明确的感知是哪一张券。格式为 JSON 数组字符串，每个 ID 长度 <= 64byte
        /// </summary>
        [JsonPropertyName("ext_ticket_ids")]
        public object ExtTicketIds { get; set; }
        /// <summary>
        /// 券的使用说明。JSON 数组字符串，最多可以有10条，每条长度 <= 50byte。必须写明券的使用条件、领取条件、退款规则，请参考示例。
        /// </summary>
        [JsonPropertyName("ticket_description")]
        public object TicketDescription { get; set; }
    }
}
