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
    /// 资源
    /// </summary>
    [PetaPoco.TableName("ResourceInfo")]
    [PetaPoco.PrimaryKey("ResourceId")]
    public partial class ResourceInfo
    {

        public int ResourceId { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName{ get; set; }
        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int DisplayOrder { get; set; }
    }

}
