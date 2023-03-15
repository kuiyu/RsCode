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
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode
{
    public class AsyncLock
    {
        private readonly Task<IDisposable> _releaserTask;
        private readonly SemaphoreSlim _semaphore;
        private readonly IDisposable _releaser;

        public AsyncLock(int initCount=1,int maxCount=1)
        {
            if (initCount < 1) initCount = 1;
            if (maxCount < 1) maxCount = 1;
            _semaphore = new SemaphoreSlim(initCount, maxCount);
            _releaser = new Releaser(_semaphore);
            _releaserTask = Task.FromResult(_releaser);
        }
        public IDisposable Lock()
        {
            _semaphore.Wait();
            return _releaser;
        }
        public Task<IDisposable> LockAsync()
        {
            var waitTask = _semaphore.WaitAsync();
            return waitTask.IsCompleted
                ? _releaserTask
                : waitTask.ContinueWith(
                    (_, releaser) => (IDisposable)releaser,
                    _releaser,
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }
        private class Releaser : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;
            public Releaser(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }
            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }

     
}
