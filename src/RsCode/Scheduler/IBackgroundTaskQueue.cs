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
using System.Threading.Tasks;

namespace RsCode
{

    public interface IBackgroundTaskQueue
        {
           /// <summary>
           /// 添加任务到队列
           /// </summary>
           /// <param name="workItem"></param>
           /// <returns></returns>
            ValueTask QueueWorkItemAsync(Func<CancellationToken, ValueTask> workItem);
        /// <summary>
        /// 任务出列
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
            ValueTask<Func<CancellationToken, ValueTask>> DequeueWorkItemAsync(CancellationToken cancellationToken);
        }
   
}
