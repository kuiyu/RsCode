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
    public class DocumentUpdateRequest
    {
        /// <summary>
        /// 待修改的知识库文件 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// 知识库文件的新名称。
        /// </summary>
        [JsonPropertyName("document_name")]
        public string DocumentName { get; set; }

        /// <summary>
        /// 在线网页更新策略，仅在上传在线网页时需要设置。默认不自动更新。详细信息可参考 UpdateRule object。
        /// </summary>
        [JsonPropertyName("update_rule")]
        public UpdateRuleObject UpdateRule { get; set; }


    }
}
