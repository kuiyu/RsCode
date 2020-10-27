using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        public CustomMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var action=DynamicApiServiceNameHelper.GetActionNameInServiceNameWithAction(context.Request.Path);
            var controller = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(context.Request.Path);
             
            //创建controller
           

            return this.next(context);
        }
    }

    public interface IA
    {
        int Add(int a, int b);
    }

    public class A : IA
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

}
