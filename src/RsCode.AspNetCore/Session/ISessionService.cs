using Microsoft.AspNetCore.Http;
using RsCode.DI;

namespace System
{
    /// <summary>
    /// 当前会话
    /// 
    /// </summary>
   public interface ISessionService:IScopedDependency
    {
        /// <summary>
        /// 当前用户id
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        void AddCookie(string CookieName, string CookieValue);
        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        /// <param name="options"></param>
        void AddCookie(string CookieName, string CookieValue, CookieOptions options);
        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="CookieName"></param>
        void DeleteCookie(string CookieName);
    }
}
