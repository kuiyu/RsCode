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

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RsCode.AspNetCore.Plugin;
using System.Reflection;
using System.Text.Json;

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
        internal static Dictionary<string, IPluginSetup> PluginSetup => _Plugins ?? (_Plugins = new Dictionary<string, IPluginSetup>());
        static Dictionary<string, PluginLoadContext> PluginAssemblyContext = new Dictionary<string, PluginLoadContext>();

        public static string PluginsRootFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
        /// <summary>
        /// Add all plugins ConfigureServices and run plugins ConfigureServices method
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        public static void AddPlugins(this IServiceCollection services, string pluginsRootFolder = "")
        {
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton<IActionDescriptorChangeProvider>(RsCodeActionDescriptionChangeProvider.Instance);
            services.AddSingleton(RsCodeActionDescriptionChangeProvider.Instance);
            services.AddSingleton<IReferenceLoader, DefaultReferenceLoader>();
            services.AddSingleton<IReferenceContainer, DefaultReferenceContainer>();

            LoadPlugins(services,pluginsRootFolder);

            foreach (var plugin in PluginSetup)
            {
                services.LoadViews(plugin);
                services.LoadControllers(plugin);

                foreach (var pluginSetup in PluginSetup.Select(x => x.Value))
                    pluginSetup.ConfigureServices(services);

            }

            services.AddControllers().AddControllersAsServices();
        }
        /// <summary>
        /// Load all plugins assembly full path and plugins Setup.cs as dictionary
        /// </summary>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        private static void LoadPlugins(IServiceCollection services, string pluginsRootFolder = "")
        {
            if (!string.IsNullOrEmpty(pluginsRootFolder))
            {
                PluginsRootFolder = pluginsRootFolder;
            }

            var pluginInfos = GetPluginInfos(pluginsRootFolder);
            foreach (var pluginInfo in pluginInfos)
            {
                var pluginPath = pluginInfo.Key.Replace("PluginInfo.json", $"{pluginInfo.Value.Name}.dll");
                var assembly=LoadPlugin(pluginPath);

                var context = GetPluginContext(pluginPath);
                
            }
        }

        private static Assembly LoadPlugin(string pluginPath)
        {
            Assembly assembly;
            using (var fs = new FileStream(pluginPath, FileMode.Open))
            {
                var context = new PluginLoadContext();
                assembly = context.LoadFromStream(fs);
                PluginAssemblyContext.Add(pluginPath, context);
            }

            TypeInfo typeInfo;
            try
            {
                typeInfo = assembly.DefinedTypes.First((dt) => dt.ImplementedInterfaces.Any(ii => ii == typeof(IPluginSetup)));
            }
            catch (Exception ex)
            {
                throw new TypeLoadException($"{nameof(LoadPlugin)}: {pluginPath} not contains a definition for {typeof(IPluginSetup).Name}", ex);
            }

            if (typeInfo == null)
                throw new TypeUnloadedException($"{nameof(LoadPlugin)}: {pluginPath} not contains a definition for {typeof(IPluginSetup).Name}");

            var pluginSetup = (IPluginSetup)assembly.CreateInstance(typeInfo.FullName);
            PluginSetup.Add(pluginPath, pluginSetup);

            return assembly;
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
                Directory.CreateDirectory(PluginsRootFolder);

            var pluginInfos = new Dictionary<string, PluginInfo>();
            var jsonPluginInfoPaths = Directory.GetFiles(PluginsRootFolder, "*PluginInfo.json", SearchOption.AllDirectories);
            foreach (var jsonPath in jsonPluginInfoPaths)
            {
                string json = File.ReadAllText(jsonPath);
                var pluginInfo =JsonSerializer.Deserialize <PluginInfo>(json,new JsonSerializerOptions
                {
                     PropertyNameCaseInsensitive= true,
                });
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
            string viewAssembypath = plugin.Key;//.Replace(".dll", ".Views.dll");
            if (File.Exists(viewAssembypath))
            {
                string pluginPath = plugin.Key;
                var pluginContext = GetPluginContext(pluginPath);                   
                var assembly =pluginContext.LoadFromAssemblyPath(pluginPath);

                var partManager = services.BuildServiceProvider().GetRequiredService<ApplicationPartManager>();
                var compiledRazorAssemblyApplicationParts = new CompiledRazorAssemblyApplicationPartFactory().GetApplicationParts(assembly);
                foreach (var item in compiledRazorAssemblyApplicationParts)
                {
                    partManager.ApplicationParts.Add(item);
                }
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
            var assembly = pluginContext.Assemblies.First();
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
            foreach (var plugin in PluginSetup.OrderBy(x => x.Value.Order).Select(x => x.Value))
                plugin.Configure(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            return app;
        }
        /// <summary>
        /// Get Plugin AssemblyPart
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static PluginAssemblyPart GetControllerPart(string pluginName)
        {
            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
            var context = new PluginLoadContext();
            var plugin = PluginAssemblyContext.FirstOrDefault(x => x.Key == pluginPath);

            //if (plugin.Key == null)
            //{
            //    using (var fs = new FileStream(pluginPath, FileMode.Open))
            //    {
            //        var assembly = context.LoadFromStream(fs);
            //        PluginAssemblyContext.Add(pluginPath, context);
            //        var assemblyPart = new PluginApplicationPart(assembly);
            //        return assemblyPart;
            //    }
            //}
            //else
            //{
            //   var assembly = plugin.Value.Assemblies.FirstOrDefault();
            //    return new PluginApplicationPart(assembly);
            //}


            var pluginContext = GetPluginContext(pluginPath);
            var assembly = pluginContext.Assemblies.First();

            return new PluginAssemblyPart(assembly);
            
        }
         
        public static IEnumerable<ApplicationPart> GetRazorPart(string pluginName)
        {
            List<ApplicationPart> parts = new List<ApplicationPart>();
            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
            var pluginContext = GetPluginContext(pluginPath);
            var assembly=pluginContext.Assemblies.FirstOrDefault();
            return new CompiledRazorAssemblyApplicationPartFactory().GetApplicationParts(assembly);
        }

        public static PluginLoadContext? GetPluginContext(string pluginPath)
        {
            try
            {
                return PluginAssemblyContext[pluginPath];
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static void InitPlugin(string pluginName)
        {
            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
            
            var pluginContext = GetPluginContext(pluginPath);
            if(pluginContext==null)
            {
                LoadPlugin(pluginPath);
            }
        }
     

        public static List<ApplicationPart> UpdatePlugin(string pluginName)
        {
            List<ApplicationPart>parts = new List<ApplicationPart>();

            var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");
           
            PluginAssemblyContext.Remove(pluginPath);
            PluginSetup.Remove(pluginPath);
            //初始化
            InitPlugin(pluginName);

            
            parts.AddRange(GetRazorPart(pluginName));
            parts.Add(GetControllerPart(pluginName));
            return parts;
        }
    }
    

}
