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

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RsCode.AspNetCore
{
	public class IdempotentFilter:IActionFilter,IResultFilter
	{
        ICacheProvider cache;
		private string _idempotencyKey;
		const string IdempotencyKeyHeaderName = "IdempotencyKey";
		private bool _isIdempotencyCache = false;
		public IdempotentFilter(IEnumerable<ICacheProvider> caches)
        {
            cache = caches.FirstOrDefault(c => c.CacheName == "memory");
        }

		private string GetDistributedCacheKey()
		{
			return "Idempotency:" + _idempotencyKey;
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			Microsoft.Extensions.Primitives.StringValues idempotencyKeys;
			
			context.HttpContext.Request.Headers.TryGetValue(IdempotencyKeyHeaderName, out idempotencyKeys);
			var userId = context.HttpContext.User.Claims?.FirstOrDefault(c=>c.Type=="UserId")?.Value;
			var url = context.HttpContext.Request.GetDisplayUrl();
			_idempotencyKey =$"{userId}{url}{idempotencyKeys.ToString()}";

			if(!string.IsNullOrWhiteSpace(_idempotencyKey))
			{
				var cacheData = cache.Get<string>(GetDistributedCacheKey());
				if (cacheData != null&&cacheData!="{}")
				{
					var obj= JsonSerializer.Deserialize<object>(cacheData);
					context.Result =new ObjectResult(obj);
					_isIdempotencyCache = true;
					
				}
			}
			
		}
		public void OnResultExecuted(ResultExecutedContext context)
		{
			if (_isIdempotencyCache)
			{
				return;
			}

			if(context.Result is ContentResult result)
			{
				 
			}else
			{
                var contextResult = (ObjectResult)context.Result;

                //缓存:
                string json = JsonSerializer.Serialize(contextResult.Value);
                cache.Set(GetDistributedCacheKey(), json, DateTime.Now.AddSeconds(10));
            }
			
		
		}

		public void OnResultExecuting(ResultExecutingContext context)
		{
			
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}

		
	}
}
