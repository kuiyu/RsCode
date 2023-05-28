using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RsCode.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Douyin.Tests
{
    public class Startup
    {
        //  public IHostBuilder CreateHostBuilder([AssemblyName assemblyName]) { }

        // 自定义 host 构建
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    // 注册配置 
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", true, true);

                })
                .ConfigureServices((context, services) =>
                {
                    // 注册自定义服务
                    services.AddRsCode();
                    services.AddDouyin();

                })

                //    .UseServiceContext(o=> 
                //{
                //    var p = Predicates.ForNameSpace("RsCode.Storage.QiniuStorage.Core");
                //    o.AddDataAnnotations(p);                
                //})
                ;

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
