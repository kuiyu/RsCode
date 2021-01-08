/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public   class UploadCompleteResponse:StorageResponse
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        /// 目标资源的 hash 值，可用于 Etag 头部
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}
