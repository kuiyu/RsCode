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

namespace RsCode
{
    public class ObjectContext<T> : IDisposable
         where T : class
    {
        static AsyncLocal<T> _scope = new AsyncLocal<T>();

        public ObjectContext(T instance)
        {
            Instance = instance;
            _scope.Value = this.Instance;
        }
        public T Instance { get; set; }

        public static T Current
        {
            get
            {
                return _scope.Value;
            }
        }
        public void Dispose()
        {
            if (Instance != null)
            {
                (Instance as IDisposable)?.Dispose();
            }
        }
    }
}
