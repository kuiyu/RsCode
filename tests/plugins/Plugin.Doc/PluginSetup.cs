using RsCode.AspNetCore;
using RsCode.AspNetCore.Plugin;

namespace Plugin.Doc
{
    internal class PluginSetup : IPluginSetup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRsCode();
        }
    }
}
