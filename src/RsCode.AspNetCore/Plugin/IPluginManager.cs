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
using RsCode.AspNetCore.Plugin;
using System.Collections.Generic;

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
