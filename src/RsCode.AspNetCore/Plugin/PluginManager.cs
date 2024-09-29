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

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Text.Json;

namespace RsCode.AspNetCore.Plugin
{
    public class PluginManager : IPluginManager
    {
        IReferenceLoader loader;
         ApplicationPartManager partManager;
       
        public PluginManager(ApplicationPartManager applicationPartManager,IReferenceLoader _loader)
        {
            partManager = applicationPartManager;
            this.loader = _loader;
            
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
                    var pluginInfo =JsonSerializer.Deserialize<T>(File.ReadAllText(jsonPath),new JsonSerializerOptions
                    {
                         PropertyNameCaseInsensitive = true
                    });
                    if(pluginInfo==null)
                        continue;

                    var pluginPart=partManager.ApplicationParts.FirstOrDefault(x => x.Name == pluginInfo.Name);
                    if (pluginPart!=null)
                    {
                        pluginInfo.Loaded = true;
                    }
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
            var plugins = partManager.ApplicationParts.Where(p => p.Name == pluginName);
            
            if (plugins != null)
            {
                for (int i = 0; i <= plugins.Count(); i++)
                {
                    var plugin=partManager.ApplicationParts.FirstOrDefault(x=>x.Name==pluginName);
                    if(plugin!=null)
                    {
                        partManager.ApplicationParts.Remove(plugin);
                    }
                }
               
                ResetControllActions();
            }
        }
        /// <summary>
        /// 启用插件
        /// </summary>
        /// <param name="pluginName"></param>
        public void EnablePlugin(string pluginName)
        {
            PluginExtensions.InitPlugin(pluginName);

            var applicationPart = partManager.ApplicationParts.FirstOrDefault(x => x.Name == pluginName);
            if(applicationPart==null)
            {
                var razorParts= PluginExtensions.GetRazorPart(pluginName);
                if(razorParts!=null)
                {
                    foreach (var item in razorParts)
                    {
                        partManager.ApplicationParts.Add(item);
                    }
                }

                var controlPart = PluginExtensions.GetControllerPart(pluginName);
                if(controlPart!=null)
                {
                    partManager.ApplicationParts.Add(controlPart);
                }
                ResetControllActions();
            }
            
        }
        
        public void UpdatePlugin(string pluginName)
        {
            DisablePlugin(pluginName); 
            var parts=PluginExtensions.UpdatePlugin(pluginName);
            foreach (var part in parts)
            {
                partManager.ApplicationParts.Add(part);
            }

            ////加载程序集引用

            //var pluginPath = $"{Path.Combine( PluginExtensions.PluginsRootFolder,pluginName)}";
            //var pluginContext=PluginExtensions.GetPluginContext($"{Path.Combine( pluginPath,pluginName+".dll")}");
            //var assembly = pluginContext.Assemblies.First();
            //loader.LoadStreamsIntoContext(pluginContext,pluginPath,assembly);



            ResetControllActions();
        }
        public bool IsLoad(string pluginName)
        {
            var pluginAssemblyPart = partManager.ApplicationParts.FirstOrDefault(p => p.Name == pluginName);
            return pluginAssemblyPart != null;
        }
        

        private void ResetControllActions()
        {
            RsCodeActionDescriptionChangeProvider.Instance.HasChanged = true;
            RsCodeActionDescriptionChangeProvider.Instance.TokenSource?.Cancel();
        }
    }

    public class ControllerNamespaceConvention : IControllerModelConvention
    {
        private readonly string _namespace;
        public ControllerNamespaceConvention(string @namespace)
        {
            _namespace = @namespace;
        }
        public void Apply(ControllerModel controllerModel)
        {
            if (controllerModel.ControllerType.Namespace.StartsWith(_namespace))
            {
                // 可以在这里进行其他处理，比如添加过滤器等
            }
        }
    }
}