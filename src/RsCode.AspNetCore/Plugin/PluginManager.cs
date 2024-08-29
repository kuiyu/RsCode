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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Newtonsoft.Json;

namespace RsCode.AspNetCore.Plugin
{
    public class PluginManager : IPluginManager
    {
        ApplicationPartManager partManager;

        public PluginManager(ApplicationPartManager applicationPartManager)
        {
            partManager = applicationPartManager;
        }
        /// <summary>
        /// 获取所有插件信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllPlugins<T>()
            where T:PluginInfo
        {
            List<T> pluginInfos = new List<T>();
            if (Directory.Exists(PluginExtensions.PluginsRootFolder))
            {
                var jsonPluginInfoPaths = Directory.GetFiles(PluginExtensions.PluginsRootFolder, "*PluginInfo.json", SearchOption.AllDirectories);
                foreach (var jsonPath in jsonPluginInfoPaths)
                {
                    var pluginInfo = JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
                    pluginInfos.Add(pluginInfo);
                }
            }

            return pluginInfos;
        }
        /// <summary>
        /// 禁用单个插件
        /// </summary>
        /// <param name="pluginName"></param>
        public void DisablePlugin(string pluginName)
        {
            var plugin = partManager.ApplicationParts.FirstOrDefault(p => p.Name == pluginName);
            if (plugin != null)
            {
                partManager.ApplicationParts.Remove(plugin);
                ResetControllActions();
            }
        }
        /// <summary>
        /// 启用插件
        /// </summary>
        /// <param name="pluginName"></param>
        public void EnablePlugin(string pluginName)
        {
            var pluginAssemblyPart = partManager.ApplicationParts.FirstOrDefault(p => p.Name == pluginName);
            if (pluginAssemblyPart == null)
            {
                pluginAssemblyPart = PluginExtensions.GetPluginAssemblyPart(pluginName);
                partManager.ApplicationParts.Add(pluginAssemblyPart);
                ResetControllActions();
            }
        }

        private void ResetControllActions()
        {
            RsCodeActionDescriptionChangeProvider.Instance.HasChanged = true;
            RsCodeActionDescriptionChangeProvider.Instance.TokenSource?.Cancel();
        }
    }
}