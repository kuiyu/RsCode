using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public  class ThreadTest
    {
        [Fact]
        public async Task AsyncLockTest()
        {
            AsyncLock m = new AsyncLock();
            
            for (int i= 1;  i<11;i++)
            {
                Thread.Sleep(1000);
                var index = i; // 定义index 避免出现闭包的问题

                Task.Run(async () =>
                {
                    using (await m.LockAsync())
                    {
                        try
                        {
                            Console.WriteLine($"第{index}个人正在过桥。");
                            Thread.Sleep(5000); // 模拟过桥需要花费的时间
                        }
                        finally
                        {
                            Console.WriteLine($"第{index}个人已经过桥。");
                        }
                    }
                });
                
               
            }
        }

     
    }
}
