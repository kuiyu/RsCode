/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RsCode
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [JsonPropertyName("currentPage")]public long CurrentPage { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonPropertyName("totalPages")] public long TotalPages { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonPropertyName("totalItems")] public long TotalItems { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonPropertyName("items")] public List<T> Items { get; set; }

  
    }
}
