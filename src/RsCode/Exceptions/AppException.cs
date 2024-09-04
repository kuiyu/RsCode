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
