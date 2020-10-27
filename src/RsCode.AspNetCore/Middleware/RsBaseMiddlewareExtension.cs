using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Middleware
{
    public static class RsBaseMiddlewareExtension
    {
        public static IApplicationBuilder UseRsBase(this IApplicationBuilder builder)
         {
            return builder.UseMiddleware<RsBaseMiddleware>();
        }
    }
}
