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

using System.Text.Json.Serialization;

namespace RsCode.AspNetCore.Plugin
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// 插件名称
        /// </summary>
       [JsonPropertyName("name")] public string Name { get; set; }
        /// <summary>
        /// 插件版本号
        /// </summary>
        [JsonPropertyName("version")] public string Version { get; set; }
        /// <summary>
        /// 插件描述
        /// </summary>
        [JsonPropertyName("description")] public string Description { get; set; }
        /// <summary>
        /// 是否己加载
        /// </summary>
        [JsonPropertyName("loaded")]
        public bool Loaded { get; set; } = false;
    }
}
