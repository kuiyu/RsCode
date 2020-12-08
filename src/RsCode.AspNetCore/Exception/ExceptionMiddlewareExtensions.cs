using Microsoft.AspNetCore.Builder;

namespace RsCode.AspNetCore
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionFilter(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }
    }
}
