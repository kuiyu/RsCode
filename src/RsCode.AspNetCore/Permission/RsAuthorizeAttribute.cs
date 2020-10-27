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
