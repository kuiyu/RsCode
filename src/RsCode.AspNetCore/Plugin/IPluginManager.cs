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

using RsCode.AspNetCore.Plugin;

namespace RsCode.AspNetCore
{
    public interface IPluginManager
    {
        /// <summary>
        /// 获取所有插件信息
        /// </summary>
        /// <returns></returns>
        List<T> GetAllPlugins<T>() where T : PluginInfo;
        /// <summary>
        /// 禁用单个插件
        /// </summary>
        /// <param name="pluginName"></param>
        void DisablePlugin(string pluginName);
        /// <summary>
        /// 启用插件
        /// </summary>
        /// <param name="pluginName"></param>
        void EnablePlugin(string pluginName);
    }
}
