using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.AspNetCore.Permission
{
    /// <summary>
    /// 资源权限配置
    /// </summary>
   public class ResourceRequirementData:IRsAuthorizeData
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 要使用的策略名称
        /// </summary>
        public string Policy { get; set; }
        /// <summary>
        /// 拥有权限的角色
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 拥有权限的组
        /// </summary>
        public string Groups { get; set; } = "*";
        public string AuthenticationSchemes { get ; set; }
    }
}
