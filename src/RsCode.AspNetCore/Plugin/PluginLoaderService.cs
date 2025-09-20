namespace RsCode.AspNetCore.Plugin
{
    public class PluginLoaderService : BackgroundService
    {
        private readonly IPluginManager _pluginManager;
        private readonly PluginOptions _options;

        public PluginLoaderService(IPluginManager pluginManager, PluginOptions options)
        {
            _pluginManager = pluginManager;
            _options = options;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 启动时加载所有已启用的插件
            foreach (var plugin in _pluginManager.GetAllPlugins<PluginInfo>())
            {
                if (plugin.Loaded)
                {
                    _pluginManager.EnablePlugin(plugin.Name);
                }
            }

            return Task.CompletedTask;
        }
    }

    public class PluginOptions
    {
        public string PluginsRootFolder { get; set; } = Path.Combine(AppContext.BaseDirectory, "Plugins");
        public string[] SharedAssemblyNames { get; set; }
        public bool EnableHotReload { get; set; } = true;
    }
}
