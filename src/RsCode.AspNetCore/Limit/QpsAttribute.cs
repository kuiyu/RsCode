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
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class QpsAttribute : Attribute,IActionFilter, IAsyncActionFilter,IResultFilter
	{
		static System.Threading.SemaphoreSlim semaphoreSlim;
        public QpsAttribute(int count=1)
        {
            semaphoreSlim= new System.Threading.SemaphoreSlim(count);
        }

		public  void OnActionExecuted(ActionExecutedContext context)
		{
			//semaphoreSlim.Release();
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			//semaphoreSlim.Wait();
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			//await semaphoreSlim.WaitAsync();
			await next();
		}

		public void OnResultExecuted(ResultExecutedContext context)
		{
			//throw new NotImplementedException();
		}

		public void OnResultExecuting(ResultExecutingContext context)
		{
			//throw new NotImplementedException();
		}
	}
}
