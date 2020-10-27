using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.AspNetCore.Permission
{
  public interface IRsAuthorizeData:IAuthorizeData
    {
        /// <summary>
        /// 资源名称
        /// </summary>
         string ResourceName { get; set; }
        /// <summary>
        /// 组名
        /// </summary>
        string Groups { get; set; }
    }
}
