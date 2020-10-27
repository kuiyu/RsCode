using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.DI
{
    /// <summary>
    /// 一个接口多个实现时的名称
    /// </summary>
   [AttributeUsage(AttributeTargets.All)]
    public class ServiceNameAttribute:Attribute
    {
        public ServiceNameAttribute(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new Exception("The ServiceName name cannot be empty");
            this.Name = serviceName;
        }
        /// <summary>
        /// 接口实现的自定义别名
        /// </summary>
        public string Name { get; set; }
    }
}
