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
