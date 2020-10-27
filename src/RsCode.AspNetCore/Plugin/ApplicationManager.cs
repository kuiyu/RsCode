using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RsCode.AspNetCore.Plugin
{
    public static class ApplicationManager
    {
        #region Fields

        private static IHostApplicationLifetime  _applicationLifetime;

        #endregion


        #region Private Methods

        private static async Task RunWebHostAsync(Func<string[], IHostBuilder> webHostBuilderCreater, string[] args)
        {
            var webHost = webHostBuilderCreater(args).Build();
            _applicationLifetime = webHost.Services.GetService<Microsoft.Extensions.Hosting.IHostApplicationLifetime>();

            await webHost.StartAsync();
        }

        #endregion


        #region Public Methods

        public static async Task RunAsync(Func<string[], IHostBuilder> webHostBuilderCreater, string[] args)
        {
            var isCancelKeyPressed = false;

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                isCancelKeyPressed = true;
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };

            while (true)
            {
#if DEBUG
                Console.WriteLine("Application is starting");
#endif
                await RunWebHostAsync(webHostBuilderCreater, args);
#if DEBUG
                Console.WriteLine("Application has been terminated");
#endif
                if (isCancelKeyPressed)
                    break;
            }
        }

        public static void Stop()
        {
            _applicationLifetime?.StopApplication();
        }

        public static void Restart()
        {
            Stop();
        }

        #endregion
    }
}