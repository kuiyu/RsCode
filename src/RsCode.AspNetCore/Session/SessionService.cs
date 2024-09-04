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
