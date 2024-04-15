/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git

 * 文档 https://rscode.cn/
 */

using System;
namespace RsCode.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    
    public class AppException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public AppException(string msg)
        {
            Message = msg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        public AppException(int status, string msg)
        {
            Status = status;
            Message = msg;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; } = 200;
        /// <summary>
        /// 响应消息
        /// </summary>
        public new string Message { get; set; }
    }
}
