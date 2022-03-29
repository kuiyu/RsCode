/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git

 * 文档 https://rscode.cn/

 */

using Microsoft.Extensions.DependencyInjection;
using System;

namespace RsCode
{
    public static class SchedulerExtensionServices
    {
        public static void AddSchedulerJob(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundTaskQueue>(new BackgroundTaskQueue(1));
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<ScheduleJob>();
            
        }

        public static void AddSchedulerJob(this IServiceCollection services,Func<IServiceProvider,IBackgroundTaskQueue> func)
        {
            services.AddSingleton<IBackgroundTaskQueue>(ctx=>
            {
                return func(ctx);
            });
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<ScheduleJob>();
        }

    }
}
