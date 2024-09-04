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

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace RsCode.AspNetCore
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class IdempotentAttribute : Attribute, IFilterFactory
	{
		public bool IsReusable => false;

       
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			var caches=(IEnumerable<ICacheProvider>)serviceProvider.GetService(typeof(IEnumerable<ICacheProvider>));
			var filter = new IdempotentFilter(caches);
			return filter;
		}
	}
}
