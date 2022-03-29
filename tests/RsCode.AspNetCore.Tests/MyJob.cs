using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.AspNetCore.Tests
{
    public class MyJob : ScheduleJob
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly CancellationToken _cancellationToken;
        public MyJob(IBackgroundTaskQueue taskQueue, ILogger<ScheduleJob> logger, IHostApplicationLifetime applicationLifetime)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }



        public override async ValueTask CustomJobAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var keyStroke = Console.ReadKey();

                if (keyStroke.Key == ConsoleKey.Spacebar)
                {
                    // Enqueue a background work item
                    await _taskQueue.QueueWorkItemAsync(BuildWorkItem);
                }
            }
        }

        private async ValueTask BuildWorkItem(CancellationToken token)
        {
          
            int delayLoop = 0;
            var guid = Guid.NewGuid().ToString();

            _logger.LogInformation("Queued Background Task {Guid} is starting.", guid);

            while (!token.IsCancellationRequested && delayLoop < 3)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if the Delay is cancelled
                }

                delayLoop++;

                _logger.LogInformation("Queued Background Task {Guid} is running. "
                                       + "{DelayLoop}/3", guid, delayLoop);
            }

            if (delayLoop == 3)
            {
                _logger.LogInformation("Queued Background Task {Guid} is complete.", guid);
            }
            else
            {
                _logger.LogInformation("Queued Background Task {Guid} was cancelled.", guid);
            }
        }
    }
}
