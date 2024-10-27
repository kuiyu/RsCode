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
    public class DocumentCreateRequest
    {
        /// <summary>
        /// 知识库 ID。在扣子平台中打开指定知识库页面，页面 URL 中 knowledge 参数后的数字就是知识库 ID。例如 https://bots.bytedance.net/space/736142423532160****/knowledge/738509371792341****，知识库 ID 为 738509371792341****。
        /// </summary>
        [JsonPropertyName("dataset_id")]
        public string DatasetId { get; set; }

        /// <summary>
        /// 待上传文件的元数据信息。数组最大长度为 10，即每次最多上传 10 个文件。详细说明可参考 DocumentBase object。支持的上传方式包括：离线文件。格式支持 pdf、txt、doc、docx 类型。在线网页。
        /// </summary>
        [JsonPropertyName("document_bases")]
        public DocumentBaseObject[] DocumentBases { get; set; }

        /// <summary>
        /// 分段规则。仅向某个知识库首次上传文件时必须设置，后续向此知识库上传文件时可以不传，默认沿用首次设置，且不支持修改。详细说明可参考 ChunkStrategy object。
        /// </summary>
        [JsonPropertyName("chunk_strategy")]
        public ChunkstrategyObject ChunkStrategy { get; set; }


    }
}
