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
