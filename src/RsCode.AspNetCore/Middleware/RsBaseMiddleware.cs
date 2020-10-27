using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Middleware
{
    public class RsBaseMiddleware
    {
        private RequestDelegate _next;

        public RsBaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
             await _next(httpContext);
        }
    }
}
