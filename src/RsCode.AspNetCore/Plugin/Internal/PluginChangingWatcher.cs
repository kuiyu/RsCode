using System;
using System.IO;
using System.Threading;

namespace RsCode.AspNetCore.Plugin.Internal
{
    internal delegate void PluginChangingWatcherEventHandler();

    internal sealed class PluginChangingWatcher
    {
        public PluginChangingWatcher(string pluginDirectory)
        {
            _pluginDirectory = pluginDirectory;
            _changeTime = new DateTime(0);
        }

        ~PluginChangingWatcher()
        {
            Stop();
        }


        #region EventHandlers

        public event PluginChangingWatcherEventHandler PluginsChanged;

        #endregion


        #region Helper Methods

        /// <summary>
        ///     Runs the action if <see cref="PluginChangingWatcher" /> has been started. Otherwise throws an exception.
        /// </summary>
        /// <param name="action">The action to run</param>
        /// <returns></returns>
        private void RunAfterCheckStartState(Action action)
        {
            if (!_started)
                throw new InvalidOperationException(HasNotBeenStarterErrorMessage);

            action();
        }

        #endregion


        #region Members

        private const string HasNotBeenStarterErrorMessage = "PluginChangingWatcher has not been started.";

        private FileSystemWatcher _fileSystemWatcher;
        private readonly string _pluginDirectory;
        private Thread _pluginReloadThread;
        private DateTime _changeTime;
        private bool _started;
        private bool _beginShutdown;
        private readonly string LockObject = "{PLUGINMANAGERLOCK}";
        private bool _justStarted = true;

        #endregion


        #region Public Methods

        /// <summary>
        ///     Initializes the watcher and triggers the plugin manager
        /// </summary>
        public void Start()
        {
            _started = true;
            StartFileSystemWatcher();

            ReloadPlugins();
        }

        /// <summary>
        ///     Shuts down the watcher
        /// </summary>
        public void Stop()
        {
            try
            {
                if (!_started && _beginShutdown)
                    return;

                _started = false;
                _fileSystemWatcher?.Dispose();
                _beginShutdown = true;
            }
            catch
            {
                // We don't want to get any exceptions thrown if unloading fails for some reason.
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        ///     The main updater thread loop.
        /// </summary>
        private void ReloadThreadLoop()
        {
            RunAfterCheckStartState(() =>
            {
                var invalidTime = new DateTime(0);
                while (!_beginShutdown)
                {
                    if (_changeTime != invalidTime && DateTime.Now > _changeTime)
                        ReloadPlugins();

                    Thread.Sleep(3000);
                }
            });
        }


        /// <summary>
        ///     Triggers the reloader to reload all plugins in the plugins directory
        /// </summary>
        private void ReloadPlugins()
        {
            RunAfterCheckStartState(() =>
            {
                lock (LockObject)
                {
                    if (!_justStarted)
                        PluginsChanged?.Invoke();
                    else
                        _justStarted = false;

                    _changeTime = new DateTime(0);
                }
            });
        }


        /// <summary>
        ///     Sets FileSystemWatcher and starts.
        /// </summary>
        private void StartFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher(_pluginDirectory)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Changed;
            _fileSystemWatcher.Created += FileSystemWatcher_Changed;

            _pluginReloadThread = new Thread(ReloadThreadLoop);
            _pluginReloadThread.Start();
        }


        /// <summary>
        ///     Handles changes to the file system in the plugin directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _changeTime = DateTime.Now + new TimeSpan(0, 0, 10);
        }

        #endregion
    }
}