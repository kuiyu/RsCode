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
    public class DocumentListResponse
    {
        /// <summary>
        /// 状态码。 0 代表调用成功。
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// 状态信息。API 调用失败时可通过此字段查看详细错误信息。
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 知识库文件列表，详细说明可参考 DocumentInfo object。
        /// </summary>
        [JsonPropertyName("document_infos")]
        public object[] DocumentInfos { get; set; }

        /// <summary>
        /// 指定知识库中的文件总数。
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }


    }
}
