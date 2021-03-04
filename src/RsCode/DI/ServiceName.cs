/*
 * 项目：.net开发基础工具类
 * 作者：河南软商网络科技有限公司
 * * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;

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
