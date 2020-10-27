using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 当前用户
    /// </summary>
   public partial  class SessionService:ISessionService
    {
        readonly IHttpContextAccessor httpContextAccessor;

        public SessionService(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        public int UserId
        {
            get
            {
                var id = httpContextAccessor.HttpContext.User.FindFirst("id");
                if (id != null)
                {
                    int userid = 0;
                    int.TryParse(id.Value, out userid);
                    return userid;
                }
                return 0;
            }
        }

        public void AddCookie(string CookieName, string CookieValue)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if(httpContext.Request.Cookies[CookieName]!=null)
            {
                httpContext.Response.Cookies.Delete(CookieName);
            }
            httpContext.Response.Cookies.Append(CookieName, CookieValue); 
        }

        public void AddCookie(string CookieName,string CookieValue,CookieOptions options)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext.Request.Cookies[CookieName] != null)
            {
                httpContext.Response.Cookies.Delete(CookieName);
            }
            httpContext.Response.Cookies.Append(CookieName, CookieValue, options);            
        }

        public void DeleteCookie(string CookieName)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext.Request.Cookies[CookieName] != null)
            {
                httpContext.Response.Cookies.Delete(CookieName);
            }
        }
    }
}
