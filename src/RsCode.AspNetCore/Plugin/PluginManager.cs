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
using System.Collections.Concurrent;
using System.Text.Json;

namespace RsCode.AspNetCore.Plugin
{
    public class PluginManager : IPluginManager
    {
        private readonly ConcurrentDictionary<string, PluginState> _pluginStates = new();
        ApplicationPartManager partManager;
      
        public PluginManager(ApplicationPartManager applicationPartManager)
        {
            partManager = applicationPartManager;
            
            LoadPluginStates();
        }

        public void AddPlugin(string pluginPath)
        {
            try
            {
                if (!File.Exists(pluginPath))
                    throw new FileNotFoundException($"Plugin file not found: {pluginPath}");

                var assembly = PluginExtensions.LoadPlugin(pluginPath);
                var pluginName = Path.GetFileNameWithoutExtension(pluginPath);

                // 添加应用到MVC
                var parts = PluginExtensions.GetPluginParts(pluginName);
                foreach (var part in parts)
                {
                    partManager.ApplicationParts.Add(part);
                }

                // 更新状态
                _pluginStates[pluginName] = new PluginState
                {
                    Enabled = true,
                    Loaded = true
                };
                SavePluginStates();

                ResetControllerActions();
            }
            catch (Exception ex)
            {
               LogHelper.Info($"Failed to add plugin: {pluginPath},Error:{ex.Message}");
                throw;
            }
        }

        public void RemovePlugin(string pluginName)
        {
            try
            {
                DisablePlugin(pluginName);

                // 获取插件上下文并卸载
                var pluginPath = GetPluginPath(pluginName);
                if (PluginExtensions.PluginAssemblyContext.TryGetValue(pluginPath, out var context))
                {
                    context.Unload();
                    PluginExtensions.PluginAssemblyContext.Remove(pluginPath);
                    PluginExtensions.PluginSetup.Remove(pluginPath);
                }

                _pluginStates.Remove(pluginName, out _);
                SavePluginStates();
            }
            catch (Exception ex)
            {
                LogHelper.Info($"Failed to remove plugin: {pluginName},Error:{ex.Message}");
                throw;
            }
        }

        private void LoadPluginStates()
        {
            var statesPath = Path.Combine(PluginExtensions.PluginsRootFolder, "pluginStates.json");
            if (File.Exists(statesPath))
            {
                var json = File.ReadAllText(statesPath);
                var states = JsonSerializer.Deserialize<Dictionary<string, PluginState>>(json);
                foreach (var state in states)
                {
                    _pluginStates[state.Key] = state.Value;
                }
            }
        }

        private void SavePluginStates()
        {
            var statesPath = Path.Combine(PluginExtensions.PluginsRootFolder, "pluginStates.json");
            var json = JsonSerializer.Serialize(_pluginStates);
            File.WriteAllText(statesPath, json);
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
               
                ResetControllerActions();
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
                ResetControllerActions();
            }
            
        }

        public void UpdatePlugin(string pluginName)
        {
            try
            {
                DisablePlugin(pluginName);

                // 卸载旧的上下文
                var pluginPath = GetPluginPath(pluginName);
                if (PluginExtensions.PluginAssemblyContext.TryGetValue(pluginPath, out var oldContext))
                {
                    oldContext.Unload();
                    PluginExtensions.PluginAssemblyContext.Remove(pluginPath);
                    PluginExtensions.PluginSetup.Remove(pluginPath);
                }
                // 3. 垃圾回收（帮助清理卸载的程序集）
                GC.Collect();
                GC.WaitForPendingFinalizers();
                // 重新加载插件
                var parts = PluginExtensions.UpdatePlugin(pluginName);
                foreach (var part in parts)
                {
                    // 移除可能存在的旧部件
                    var existingPart = partManager.ApplicationParts.FirstOrDefault(p => p.Name == part.Name);
                    if (existingPart != null)
                    {
                        partManager.ApplicationParts.Remove(existingPart);
                    }

                    // 添加新部件
                    partManager.ApplicationParts.Add(part);
                }
                // 4. 重新执行插件配置
                if (PluginExtensions.PluginSetup.TryGetValue(pluginPath, out var pluginSetup))
                {
                    // 创建临时服务集合来执行配置
                    var tempServices = new ServiceCollection();
                    pluginSetup.ConfigureServices(tempServices);

                    // 将服务注册应用到主服务容器
                    // 注意：这需要更复杂的实现来合并服务
                }
                // 更新状态
                if (_pluginStates.ContainsKey(pluginName))
                {
                    _pluginStates[pluginName].Loaded = true;
                    _pluginStates[pluginName].LastUpdated = DateTime.UtcNow;
                }
                else
                {
                    _pluginStates[pluginName] = new PluginState
                    {
                        Loaded = true,
                        Enabled = true,
                        LastUpdated = DateTime.UtcNow
                    };
                }

                SavePluginStates();
                ResetControllerActions();

                LogHelper.Info($"Plugin updated: {pluginName}");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, $"Failed to update plugin: {pluginName}");
                throw;
            }
        }
        public bool IsLoad(string pluginName)
        {
            var pluginAssemblyPart = partManager.ApplicationParts.FirstOrDefault(p => p.Name == pluginName);
            return pluginAssemblyPart != null;
        }

        private string GetPluginPath(string pluginName)
        {
            return Path.Combine(PluginExtensions.PluginsRootFolder, pluginName, $"{pluginName}.dll");
        }
        private void ResetControllerActions()
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