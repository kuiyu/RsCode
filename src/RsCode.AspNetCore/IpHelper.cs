using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RsCode.AspNetCore
{
    public class IpHelper
    {
        public static string GetIp(HttpContext httpContext)
        {
            return httpContext?.Request.Headers["X-Real-IP"].FirstOrDefault() ?? httpContext?.Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
