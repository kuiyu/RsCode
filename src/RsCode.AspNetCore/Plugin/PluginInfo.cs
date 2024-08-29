/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
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
    }
}
