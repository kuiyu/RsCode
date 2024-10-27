/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class MessageListRequest
    {
        /// <summary>
        /// 消息列表的排序方式。desc：（默认）按创建时间倒序asc：按创建时间正序
        /// </summary>
        [JsonPropertyName("order")]
        public string Order { get; set; }

        /// <summary>
        /// 待查看的 Chat ID。
        /// </summary>
        [JsonPropertyName("chat_id")]
        public string ChatId { get; set; }

        /// <summary>
        /// 查看指定位置之前的消息。默认为 0，表示不指定位置。如需向前翻页，则指定为返回结果中的 first_id。
        /// </summary>
        [JsonPropertyName("before_id")]
        public string BeforeId { get; set; }

        /// <summary>
        /// 查看指定位置之后的消息。默认为 0，表示不指定位置。如需向后翻页，则指定为返回结果中的 last_id。
        /// </summary>
        [JsonPropertyName("after_id")]
        public string AfterId { get; set; }

        /// <summary>
        /// 每次查询返回的数据量。默认为 50，取值范围为 1~50。
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }


    }
}
