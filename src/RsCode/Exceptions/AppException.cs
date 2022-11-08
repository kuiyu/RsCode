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
