﻿using AspectCore.Extensions.DependencyInjection;
using AspectCore.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace RsCode.AspNetCore.Tests
{
    public class Startup
    {
        // 自定义 HostBuilder ，可以没有这个方法，没有这个方法会使用默认的 hostBuilder，通常直接使用 `ConfigureHost` 应该就够用了
        // public IHostBuilder CreateHostBuilder()
        // {
        //     return new HostBuilder()
        //         .ConfigureAppConfiguration(builder =>
        //         {
        //             // 注册配置
        //             builder
        //                 .AddInMemoryCollection(new Dictionary<string, string>()
        //                 {
        //                     {"UserName", "Alice"}
        //                 })
        //                 .AddJsonFile("appsettings.json")
        //                 ;
        //         })
        //         .ConfigureServices((context, services) =>
        //         {
        //             // 注册自定义服务
        //             services.AddSingleton<IIdGenerator, GuidIdGenerator>();
        //             if (context.Configuration.GetAppSetting<bool>("XxxEnabled"))
        //             {
        //                 services.AddSingleton<IUserIdProvider, EnvironmentUserIdProvider>();
        //             }
        //         })
        //         ;
        // }

        // 自定义 host 构建
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseServiceProviderFactory(new DynamicProxyServiceProviderFactory())
                .ConfigureAppConfiguration((context,config) =>
                {
                    // 注册配置
                    config.AddJsonFile("appsettings.json", false, true);
                    
                })
                .ConfigureServices((context, services) =>
                {
                    
                    // 注册自定义服务
                    services.AddRsCode();
                    services.AddSchedulerJob();
                    services.AddSingleton<MyJob>();

                    services.AddSingleton<IA, A>();
                    services.AddSingleton<IA, B>();
                    services.AddSingleton<IA, C>();
                    
                    services.AddDatabase(FreeSql.DataType.MySql, "DefaultConnection");
                    services.AddDatabase(FreeSql.DataType.Sqlite, "DefaultConnection2",true);
                    services.AddUnitOfWork();

                    services.AddScoped<IUserService, UserService>();
                    services.AddAspectServiceContext();
                })
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
