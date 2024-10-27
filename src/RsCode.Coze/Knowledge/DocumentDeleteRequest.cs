/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class DocumentDeleteRequest
    {
        /// <summary>
        /// 待删除的知识库文件列表。数组最大长度为 100，即一次性最多可删除 100 个文件。
        /// </summary>
        [JsonPropertyName("document_ids")]
        public string[] DocumentIds { get; set; }
    }
}
