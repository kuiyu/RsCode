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
