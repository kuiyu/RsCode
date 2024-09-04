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

using Microsoft.AspNetCore.Mvc;

namespace RsCode.AspNetCore.Permission
{
    /// <summary>
    /// 授权
    /// </summary> 
    //[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class RsAuthorizeAttribute : TypeFilterAttribute
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">资源名称</param>
        public RsAuthorizeAttribute(string ResourceName) : base(typeof(ResourceRequirementFilter))        {    
            if(string.IsNullOrWhiteSpace(ResourceName))
            {
                ResourceName = "*";
            }
            Arguments = new object[] {
               new ResourceRequirementData{ Roles="*",Policy="RsPolicy",Groups="*",ResourceName=ResourceName }
            };
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResourceName">资源名称</param>
        /// <param name="Groups">允许访问的组</param>
        /// <param name="Policy">使用的策略</param>
        public RsAuthorizeAttribute(string ResourceName, string Groups="*",string Policy="RsPolicy") : base(typeof(ResourceRequirementFilter))
        {
            if (string.IsNullOrWhiteSpace(ResourceName))
            {
                ResourceName = "*";
            }
            Arguments = new object[] {
               new ResourceRequirementData{ Roles="*",Policy=Policy,Groups=Groups,ResourceName=ResourceName }
            };
        }
         

    }
}
