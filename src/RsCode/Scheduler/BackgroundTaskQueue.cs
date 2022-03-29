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

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RsCode
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        readonly Channel<Func<CancellationToken, ValueTask>> queue;
        public BackgroundTaskQueue(int capacity)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
        }

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueWorkItemAsync(CancellationToken cancellationToken)
        {
            var workItem = await queue.Reader.ReadAsync(cancellationToken);
            return workItem;
        }

        public async ValueTask QueueWorkItemAsync(Func<CancellationToken, ValueTask> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }
            await queue.Writer.WriteAsync(workItem);
        }
    }
}
