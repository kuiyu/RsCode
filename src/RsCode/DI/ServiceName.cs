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
