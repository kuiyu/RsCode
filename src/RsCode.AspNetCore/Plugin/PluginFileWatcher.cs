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


namespace RsCode.AspNetCore.Plugin
{
    public class PluginFileWatcher : IDisposable
    {
        private readonly FileSystemWatcher _watcher;
        private readonly IPluginManager _pluginManager;
        private readonly ILogger<PluginFileWatcher> _logger;

        public PluginFileWatcher(IPluginManager pluginManager,
                               ILogger<PluginFileWatcher> logger)
        {
            _pluginManager = pluginManager;
            _logger = logger;

            _watcher = new FileSystemWatcher(PluginExtensions.PluginsRootFolder)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                IncludeSubdirectories = true,
                Filter = "*.dll"
            };

            _watcher.Changed += OnPluginChanged;
            _watcher.Created += OnPluginCreated;
            _watcher.Deleted += OnPluginDeleted;
            _watcher.EnableRaisingEvents = true;
        }

        private void OnPluginChanged(object sender, FileSystemEventArgs e)
        {
            // 防抖处理
            Debouncer.Debounce(() =>
            {
                var pluginName = Path.GetFileNameWithoutExtension(e.Name);
                _logger.LogInformation($"Plugin changed: {pluginName}, reloading...");
                try
                {
                    _pluginManager.UpdatePlugin(pluginName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to reload plugin: {pluginName}");
                }
            }, TimeSpan.FromSeconds(1));
        }

        private void OnPluginCreated(object sender, FileSystemEventArgs e)
        {
            var pluginName = Path.GetFileNameWithoutExtension(e.Name);
            _logger.LogInformation($"New plugin detected: {pluginName}");
            // 可以选择自动加载新插件
        }

        private void OnPluginDeleted(object sender, FileSystemEventArgs e)
        {
            var pluginName = Path.GetFileNameWithoutExtension(e.Name);
            _logger.LogInformation($"Plugin deleted: {pluginName}");
            try
            {
                _pluginManager.RemovePlugin(pluginName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to remove plugin: {pluginName}");
            }
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }
    }

    // 防抖工具类
    public static class Debouncer
    {
        private static readonly Dictionary<string, System.Timers.Timer> _timers = new();

        public static void Debounce(Action action, TimeSpan interval, string key = "")
        {
            if (_timers.TryGetValue(key, out var timer))
            {
                timer.Stop();
                timer.Dispose();
            }

            timer = new System.Timers.Timer(interval.TotalMilliseconds)
            {
                AutoReset = false
            };

            timer.Elapsed += (s, e) =>
            {
                action();
                _timers.Remove(key);
            };

            _timers[key] = timer;
            timer.Start();
        }
    }
}
