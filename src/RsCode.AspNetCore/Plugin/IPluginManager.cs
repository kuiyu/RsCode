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
    /// <summary>
    /// 插件项目管理
    /// 可以对运行的插件开启与关闭
    /// TODO:动态添加
    /// </summary>
    public interface IPluginManager
    {
        void AddPlugin(string pluginPath);
        void RemovePlugin(string pluginName);
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
        /// <summary>
        /// 重新加载插件
        /// </summary>
        /// <param name="pluginName"></param>
        void UpdatePlugin(string pluginName);

        /// <summary>
        /// 插件是否加载
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        bool IsLoad(string pluginName);
    }
}
