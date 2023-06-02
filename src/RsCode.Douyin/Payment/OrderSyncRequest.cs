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
    /// 担保支付 订单同步请求
    /// </summary>
    public class OrderSyncRequest : DouyinRequest
    {
        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/order/v2/push";
        }

        /// <summary>
        /// 第三方在抖音开放平台申请的 ClientKey
        ///注意：POI 订单必传
        /// </summary>
        [JsonPropertyName("client_key")]
        public string ClientKey { get; set; }
        /// <summary>
        /// 服务端 API 调用标识，通过 getAccessToken 获取
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// POI 店铺同步时使用的开发者侧店铺 ID，购买店铺 ID，长度 < 256 byte   注意：POI 订单必传
        /// </summary>
        [JsonPropertyName("ext_shop_id")]
        public string ExtShopId { get; set; }
        /// <summary>
        /// 做订单展示的字节系 app 名称，目前为固定值“douyin”
        /// </summary>
        [JsonPropertyName("app_name")]
        public string AppName { get; set; } = "douyin";
        /// <summary>
        /// 小程序用户的 open_id，通过 code2Session 获取
        /// </summary>
        [JsonPropertyName("open_id")]
        public string OpenId { get; set; }
        /// <summary>
        /// json string，根据不同订单类型有不同的结构体，请参见 order_detail 字段说明（json string）
        /// </summary>
        [JsonPropertyName("order_detail")]
        public string OrderDetail { get; set; }
        /// <summary>
        /// 普通小程序订单订单状态，POI 订单可以忽略
        ///0：待支付
        ///1：已支付
        ///2：已取消（用户主动取消或者超时未支付导致的关单）
        ///4：已核销（核销状态是整单核销,即一笔订单买了 3 个券，核销是指 3 个券核销的整单）
        ///5：退款中
        ///6：已退款
        ///8：退款失败
        ///注意：普通小程序订单必传，担保支付分账依赖该状态
        /// </summary>
        [JsonPropertyName("order_status")]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单类型，枚举值:

        ///0：普通小程序订单（非POI订单）
        ///9101：团购券订单（POI 订单）
        ///9001：景区门票订单（POI订单）
        /// </summary>
        [JsonPropertyName("order_type")]
        public int OrderType { get; set; }
        /// <summary>
        /// 订单信息变更时间，13 位毫秒级时间戳
        /// </summary>
        [JsonPropertyName("update_time")]
        public long UpdateTime { get; set; }
        /// <summary>
        /// 自定义字段，用于关联具体业务场景下的特殊参数，长度 < 2048byte
        /// </summary>
        [JsonPropertyName("extra")]
        public string Extra { get; set; }

    }
}
