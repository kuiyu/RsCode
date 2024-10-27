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
    public class DocumentListRequest
    {
        /// <summary>
        /// 待查看文件的知识库 ID。
        /// </summary>
        [JsonPropertyName("dataset_id")]
        public string DatasetId { get; set; }

        /// <summary>
        /// 分页查询时的页码。默认为 1，即从第一页数据开始返回。
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// 分页大小。默认为 10，即每页返回 10 条数据。
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }


    }
}
