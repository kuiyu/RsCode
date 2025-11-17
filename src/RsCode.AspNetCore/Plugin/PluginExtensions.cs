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
        public static Dictionary<string, PluginLoadContext> PluginAssemblyContext = new Dictionary<string, PluginLoadContext>();

        public static string PluginsRootFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
        /// <summary>
        /// Add all plugins ConfigureServices and run plugins ConfigureServices method
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="sharedAssemblyNames">主项目与子项目共用的程序集名称</param>
        /// <param name="pluginsRootFolder">Plugins root path</param>
        public static void AddPlugins(this IServiceCollection services,string[] sharedAssemblyNames=null, string pluginsRootFolder = "")
        {

            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton<IActionDescriptorChangeProvider>(RsCodeActionDescriptionChangeProvider.Instance);
            services.AddSingleton(RsCodeActionDescriptionChangeProvider.Instance);

            ReferenceLoader.AddSharedAssembly(sharedAssemblyNames);
            LoadPlugins(services, pluginsRootFolder);

            //foreach (var plugin in PluginSetup)
            //{
            //    services.LoadViews(plugin);
            //    services.LoadControllers(plugin);

            //    foreach (var pluginSetup in PluginSetup.Select(x => x.Value))
            //        pluginSetup.ConfigureServices(services);

            //}


            // 先加载所有插件的视图和控制器
            foreach (KeyValuePair<string, IPluginSetup> item in PluginSetup)
            {
                services.LoadViews(item);
                services.LoadControllers(item);
            }

            // 然后为每个插件调用 ConfigureServices（只调用一次）
            foreach (IPluginSetup pluginSetup in PluginSetup.Values)
            {
                pluginSetup.ConfigureServices(services);
            }



            services.AddControllers().AddControllersAsServices();

            // 文件监视器（单例）
            services.AddSingleton<PluginFileWatcher>();

            // 启动时加载所有插件
            services.AddHostedService<PluginLoaderService>();

            var options = new PluginOptions();
            services.AddSingleton(options);

            // 注册代理
            services.AddSingleton<ServiceRegistrationProxy>();

            // 应用插件服务注册
            var proxy = services.LastOrDefault(s => s.ServiceType == typeof(ServiceRegistrationProxy))?.ImplementationInstance as ServiceRegistrationProxy;
            proxy?.ApplyRegistrations(services);
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


            var proxy = new ServiceRegistrationProxy(); // 创建临时代理
            foreach (var pluginInfo in pluginInfos)
            {
                var pluginPath = pluginInfo.Key.Replace("PluginInfo.json", $"{pluginInfo.Value.Name}.dll");
                var assembly=LoadPlugin(pluginPath);
                var context = GetPluginContext(pluginPath);

                
                // 执行插件配置并捕获注册操作
                if (PluginSetup.TryGetValue(pluginPath, out var setup))
                {
                    proxy.AddRegistration(s => setup.ConfigureServices(s));
                }
            }

            services.AddSingleton(proxy);
        }

        public static Assembly LoadPlugin(string pluginPath)
        {
            Assembly assembly;
            TypeInfo typeInfo;
            try
            {
                using var fs = new FileStream(pluginPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                
                    var context = new PluginLoadContext(pluginPath);
                    assembly = context.LoadFromStream(fs);

                    PluginAssemblyContext.Add(pluginPath, context);

                var assemblyName = assembly.GetName().Name;
                string dllPath = pluginPath.Replace(assemblyName + ".dll", "");

                ReferenceLoader.LoadStreamsIntoContext(context, assembly, dllPath);

                // 使用共享程序集中的接口类型进行查找
                var sharedInterfaceType = typeof(IPluginSetup);

                // 方法1：查找实现 IPluginSetup 接口的类
                typeInfo = assembly.DefinedTypes
                    .FirstOrDefault(dt => dt.ImplementedInterfaces
                        .Any(ii => ii.FullName == sharedInterfaceType.FullName));

                // 方法2：如果方法1找不到，尝试使用更宽松的匹配
                if (typeInfo == null)
                {
                    typeInfo = assembly.DefinedTypes
                        .FirstOrDefault(dt => dt.GetInterfaces()
                            .Any(i => i.Name == nameof(IPluginSetup)));
                }

                //// 方法3：通过特性或命名约定查找
                //if (typeInfo == null)
                //{
                //    typeInfo = assembly.DefinedTypes
                //        .FirstOrDefault(dt => dt.Name.EndsWith("Setup") || dt.Name.EndsWith("Plugin"));
                //}
            }
            catch (Exception ex)
            {
                var err = ex.Message;Console.WriteLine(err);
                throw new TypeLoadException($"{nameof(LoadPlugin)}: {pluginPath} not contains a definition for {typeof(RsCode.AspNetCore.Plugin.IPluginSetup).Name}", ex);
            }

            if (typeInfo == null)
                throw new TypeUnloadedException($"{nameof(LoadPlugin)}: {pluginPath} not contains a definition for {typeof(RsCode.AspNetCore.Plugin.IPluginSetup).Name}");

            var pluginSetup =(IPluginSetup) assembly.CreateInstance(typeInfo.FullName);
            //var pluginSetup = (IPluginSetup)Activator.CreateInstance(typeInfo);
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
            try
            {
                var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");

                // 如果上下文不存在，先加载插件
                if (!PluginAssemblyContext.ContainsKey(pluginPath))
                {
                    LoadPlugin(pluginPath);
                }

                var pluginContext = GetPluginContext(pluginPath);
                if (pluginContext == null || !pluginContext.Assemblies.Any())
                {
                    return null;
                }

                var assembly = pluginContext.Assemblies.First();
                return new PluginAssemblyPart(assembly);
            }
            catch (Exception ex)
            {
                // 记录日志
                Console.WriteLine($"Error getting controller part for {pluginName}: {ex.Message}");
                return null;
            }
            
        }
         
        public static IEnumerable<ApplicationPart> GetRazorPart(string pluginName)
        {
            try
            {
                var pluginPath = Path.Combine(PluginsRootFolder, pluginName, $"{pluginName}.dll");

                // 如果上下文不存在，先加载插件
                if (!PluginAssemblyContext.ContainsKey(pluginPath))
                {
                    LoadPlugin(pluginPath);
                }

                var pluginContext = GetPluginContext(pluginPath);
                if (pluginContext == null || !pluginContext.Assemblies.Any())
                {
                    return Enumerable.Empty<ApplicationPart>();
                }

                var assembly = pluginContext.Assemblies.First();
                return new CompiledRazorAssemblyApplicationPartFactory().GetApplicationParts(assembly);
            }
            catch (Exception ex)
            {
                // 记录日志
                Console.WriteLine($"Error getting razor part for {pluginName}: {ex.Message}");
                return Enumerable.Empty<ApplicationPart>();
            }
        }

        public static PluginLoadContext? GetPluginContext(string pluginPath)
        {
            try
            {
                if (PluginAssemblyContext.TryGetValue(pluginPath, out var context))
                {
                    return context;
                }
                return null;
               
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



        /// <summary>
        /// 获取插件的所有应用部件
        /// </summary>
        public static List<ApplicationPart> GetPluginParts(string pluginName)
        {
            var parts = new List<ApplicationPart>();

            // 获取控制器部件
            var controllerPart = GetControllerPart(pluginName);
            if (controllerPart != null)
            {
                parts.Add(controllerPart);
            }

            // 获取Razor部件
            var razorParts = GetRazorPart(pluginName);
            if (razorParts != null && razorParts.Any())
            {
                parts.AddRange(razorParts);
            }

            return parts;
        }


    }
    

}
