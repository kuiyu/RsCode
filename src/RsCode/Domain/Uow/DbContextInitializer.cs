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

namespace RsCode.Domain.Uow
{
    public class DbContextInitializer
    {
        private static readonly object SyncLock = new object();
        private static DbContextInitializer _instance;

        protected DbContextInitializer() { }

        private bool _isInitialized = false;

        public static DbContextInitializer Instance()
        {
            if (_instance == null)
            {
                lock (SyncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new DbContextInitializer();
                    }
                }
            }

            return _instance;
        }

        public void InitializeDbContextOnce(Action initMethod)
        {
            lock (SyncLock)
            {
                if (!_isInitialized)
                {
                    initMethod();
                    _isInitialized = true;
                }
            }
        }

    }
}
