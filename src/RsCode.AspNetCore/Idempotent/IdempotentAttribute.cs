/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
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
			var filter = new IdempotentAttributeFilter(caches);
			return filter;
		}
	}
}
