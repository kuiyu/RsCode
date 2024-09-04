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
using Microsoft.Extensions.DependencyInjection;

namespace RsCode
{
    /// <summary>
    /// The services containing this <c>Attribute</c> will be ignored during service registration in <see cref="IServiceCollection"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoreServiceRegistrationAttribute : Attribute
    {
    }
}
