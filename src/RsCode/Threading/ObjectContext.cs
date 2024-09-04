/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

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
