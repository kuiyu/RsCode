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
