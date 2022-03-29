﻿/*
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

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode
{

    public class QueuedHostedService : BackgroundService
    {
        readonly ILogger<QueuedHostedService> logger;
        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
            ILogger<QueuedHostedService> _logger)
        {
            TaskQueue = taskQueue;
            logger = _logger;
        }
        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
             $"Queued Hosted Service is running.{Environment.NewLine}");

            await BackgroundProcessing(stoppingToken);
        }

        async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem =
                    await TaskQueue.DequeueWorkItemAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        $"Error occurred executing {0}.", nameof(workItem));
                }
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}
