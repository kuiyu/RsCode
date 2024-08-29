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
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RsCode.AspNetCore.Plugin;
using System.Reflection;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// Plugins manager extensions
    /// https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/mvc/advanced/app-parts.md
    /// </summary>
    public static class PluginExtensions
    {
        private static Dictionary<string, IPluginSetup> _Plugins;
        /// <summary>
        /// Loaded plugin list as dictionary key:plugin assembly full path, value:plugin Setup.cs
        /// </summary>
        internal static Dictionary<string, IPluginSetup> Plugins => _Plugins ?? (_Plugins = new Dictionary<string, IPluginSetup>());
        static Dictionary<string, PluginAssemblyLoadContext> pluginContext = new Dictionary<string, PluginAssemblyLoadContext>();

        public static string PluginsRootFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
        /// <summary>
        /// Add all plugins ConfigureServices and run plugins ConfigureServices method
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        public static void AddPlugins(this IServiceCollection services, string pluginsRootFolder = "")
        {
            services.AddScoped<IPluginManager, PluginManager>();
            services.AddSingleton<IActionDescriptorChangeProvider>(RsCodeActionDescriptionChangeProvider.Instance);
            services.AddSingleton(RsCodeActionDescriptionChangeProvider.Instance);

            LoadPlugins(pluginsRootFolder);

            foreach (var plugin in Plugins)
            {
                services.LoadViews(plugin);
                services.LoadControllers(plugin);

                foreach (var pluginSetup in Plugins.Select(x => x.Value))
                    pluginSetup.ConfigureServices(services);
            }
        }
        /// <summary>
        /// Load all plugins assembly full path and plugins Setup.cs as dictionary
        /// </summary>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        private static void LoadPlugins(string pluginsRootFolder = "")
        {
            if (!string.IsNullOrEmpty(pluginsRootFolder))
            {
                PluginsRootFolder = pluginsRootFolder;
            }

            var pluginInfos = GetPluginInfos(pluginsRootFolder);
            foreach (var pluginInfo in pluginInfos)
            {
                var pluginPath = pluginInfo.Key.Replace("PluginInfo.json", $"{pluginInfo.Value.Name}.dll");
                var context = new PluginAssemblyLoadContext();

                Assembly assembly;
                using (var fs = new FileStream(pluginPath, FileMode.Open))
                {
                    assembly = context.LoadFromStream(fs);
                    pluginContext.Add(pluginPath, context);
                }


                TypeInfo typeInfo;
                try
                {
                    typeInfo = assembly.DefinedTypes.First((dt) => dt.ImplementedInterfaces.Any(ii => ii == typeof(IPluginSetup)));
                }
                catch (Exception ex)
                {
                    throw new TypeLoadException($"{nameof(LoadPlugins)}: {pluginPath} not contains a definition for {typeof(IPluginSetup).Name}", ex);
                }

                if (typeInfo == null)
                    throw new TypeUnloadedException($"{nameof(LoadPlugins)}: {pluginPath} not contains a definition for {typeof(IPluginSetup).Name}");

                var pluginSetup = (IPluginSetup)assembly.CreateInstance(typeInfo.FullName);
                Plugins.Add(pluginPath, pluginSetup);


            }
        }
        /// <summary>
        /// Load all plugins info as PluginInfo.json
        /// </summary>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        /// <returns>plugins dictionary key:PluginInfo.json full path, value:PluginInfo.json file as PluginInfo class object</returns>
        private static Dictionary<string, PluginInfo> GetPluginInfos(string pluginsRootFolder)
        {
            if(!string.IsNullOrWhiteSpace(pluginsRootFolder))
            {
                PluginsRootFolder = pluginsRootFolder;
            }
            if (!Directory.Exists(PluginsRootFolder))
                throw new DirectoryNotFoundException($"{nameof(GetPluginInfos)}: no Plugins folder found");

            var pluginInfos = new Dictionary<string, PluginInfo>();
            var jsonPluginInfoPaths = Directory.GetFiles(PluginsRootFolder, "*PluginInfo.json", SearchOption.AllDirectories);
            foreach (var jsonPath in jsonPluginInfoPaths)
            {
                var pluginInfo = JsonConvert.DeserializeObject<PluginInfo>(File.ReadAllText(jsonPath));
                pluginInfos.Add(jsonPath, pluginInfo);
            }

            return pluginInfos;
        }
        /// <summary>
        /// Load pugin views
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="plugin">plugin object key:plugin full path, value:plugin Setup.cs</param>
        private static void LoadViews(this IServiceCollection services, KeyValuePair<string, IPluginSetup> plugin)
        {
            string viewAssembypath = plugin.Key.Replace(".dll", ".Views.dll");
            if (File.Exists(viewAssembypath))
            {
                string pluginPath = plugin.Key;
                var pluginContext = GetPluginContext(pluginPath);
                var assembly = pluginContext.Assemblies.FirstOrDefault();

                services.AddMvcCore().ConfigureApplicationPartManager(apm =>
                {
                    var compiledRazorAssemblyApplicationParts = new CompiledRazorAssemblyApplicationPartFactory().GetApplicationParts(assembly);
                    foreach (var craapf in compiledRazorAssemblyApplicationParts)
                        apm.ApplicationParts.Add(craapf);
                });
            }
        }
        /// <summary>
        /// Load plugin controllers
        /// https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="plugin">plugin object key:plugin full path, value:plugin Setup.cs</param>
        private static void LoadControllers(this IServiceCollection services, KeyValuePair<string, IPluginSetup> plugin)
        { 
            string pluginPath = plugin.Key;
            var pluginContext = GetPluginContext(pluginPath);
            var assembly = pluginContext.Assemblies.FirstOrDefault();
            var assemblyPart = new PluginAssemblyPart(assembly);
            services
                .AddMvcCore()
                .ConfigureApplicationPartManager(apm => apm
                    .ApplicationParts.Add(assemblyPart)
                );
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline for plugin
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the hosting environment an application is running in</param>
        /// <returns>Defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
        public static IApplicationBuilder UsePlugins(this IApplicationBuilder app, IHostEnvironment env)
        {
            foreach (var plugin in Plugins.OrderBy(x => x.Value.Order).Select(x => x.Value))
                plugin.Configure(app, env);

            return app;
        }
        /// <summary>
        /// Get Plugin AssemblyPart
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static PluginAssemblyPart GetPluginAssemblyPart(string pluginName)
        {
            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
            var context = new PluginAssemblyLoadContext();
            var plugin = pluginContext.FirstOrDefault(x => x.Key == pluginPath);

            if (plugin.Key == null)
            {
                using (var fs = new FileStream(pluginPath, FileMode.Open))
                {
                    var assembly = context.LoadFromStream(fs);
                    pluginContext.Add(pluginPath, context);
                    var assemblyPart = new PluginAssemblyPart(assembly);
                    return assemblyPart;
                }
            }
            else
            {
                var assembly = plugin.Value.Assemblies.FirstOrDefault();
                return new PluginAssemblyPart(assembly);
            }

        }

        public static PluginAssemblyPart UpdatePluginAssemblyPart(string pluginName)
        {
            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
            var context = new PluginAssemblyLoadContext();
            var plugin = pluginContext.FirstOrDefault(x => x.Key == pluginPath);
            if (plugin.Key != null)
                pluginContext.Remove(pluginPath);

            using (var fs = new FileStream(pluginPath, FileMode.Open))
            {
                var assembly = context.LoadFromStream(fs);
                pluginContext.Add(pluginPath, context);
                var assemblyPart = new PluginAssemblyPart(assembly);
                return assemblyPart;
            }
        }
        public static PluginAssemblyLoadContext GetPluginContext(string pluginName)
        {
            return pluginContext[pluginName];
        }


    }
}
