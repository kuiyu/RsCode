using RsCode.AspNetCore;
using RsCode.AspNetCore.Plugin;

namespace Plugin.Discount
{
    internal class PluginSetup : IPluginSetup
    {
        public int Order => 200;

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRsCode();
        }
    }
}
