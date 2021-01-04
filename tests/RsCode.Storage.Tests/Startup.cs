using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using AspectCore.Extensions.Hosting;
using AspectCore.Extensions.DataAnnotations;
using AspectCore.Extensions.DependencyInjection;

namespace RsCode.Storage.Tests
{
    public class Startup
    {

      //  public IHostBuilder CreateHostBuilder([AssemblyName assemblyName]) { }

        // 自定义 host 构建
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceContext(o=>o.AddDataAnnotations())
                .ConfigureAppConfiguration(builder =>
                {
                    // 注册配置 
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", true, true);

                })
                .ConfigureServices((context, services) =>
                {
                    // 注册自定义服务
                    services.AddQiniuStorage(context.Configuration);
                    services.AddAspectServiceContext();
                });

        }
            
        // 支持的形式：
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            //services.TryAddSingleton<CustomService>();
           
        }

        // 可以添加要用到的方法参数，会自动从注册的服务中获取服务实例，类似于 asp.net core 里 Configure 方法
        public void Configure(IServiceProvider applicationServices)
        {
            // 有一些测试数据要初始化可以放在这里
            // InitData();
        }
    }
}
