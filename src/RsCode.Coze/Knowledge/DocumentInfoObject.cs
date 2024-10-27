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
    public class DocumentInfoObject
    {
        /// <summary>
        /// 文件内容的总字符数量。
        /// </summary>
        [JsonPropertyName("char_count")]
        public int CharCount { get; set; }

        /// <summary>
        /// 分段规则。详细说明可参考 chunk_strategy object。
        /// </summary>
        [JsonPropertyName("chunk_strategy")]
        public object ChunkStrategy { get; set; }

        /// <summary>
        /// 文件的上传时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("create_time")]
        public int CreateTime { get; set; }

        /// <summary>
        /// 文件的 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// 文件的格式类型。取值包括：0：文档类型，例如 txt 、pdf 、在线网页等格式均属于文档类型。1：表格类型，例如 xls 表格等格式属于表格类型。2：照片类型，例如 png 图片等格式属于照片类型。
        /// </summary>
        [JsonPropertyName("format_type")]
        public int FormatType { get; set; }

        /// <summary>
        /// 被对话命中的次数。
        /// </summary>
        [JsonPropertyName("hit_count")]
        public int HitCount { get; set; }

        /// <summary>
        /// 文件的名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 文件的大小，单位为字节。
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// 文件的分段数量。
        /// </summary>
        [JsonPropertyName("slice_count")]
        public int SliceCount { get; set; }

        /// <summary>
        /// 文件的上传方式。取值包括：0：上传本地文件。1：上传在线网页。
        /// </summary>
        [JsonPropertyName("source_type")]
        public int SourceType { get; set; }

        /// <summary>
        /// 文件的处理状态。取值包括：0：处理中1：处理完毕9：处理失败，建议重新上传
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// 本地文件格式，即文件后缀，例如 txt。格式支持 pdf、txt、doc、docx 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 在线网页自动更新的频率。单位为小时。
        /// </summary>
        [JsonPropertyName("update_interval")]
        public int UpdateInterval { get; set; }

        /// <summary>
        /// 文件的最近一次修改时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("update_time")]
        public int UpdateTime { get; set; }

        /// <summary>
        /// 在线网页是否自动更新。取值包括：0：不自动更新1：自动更新
        /// </summary>
        [JsonPropertyName("update_type")]
        public int UpdateType { get; set; }


    }
}
