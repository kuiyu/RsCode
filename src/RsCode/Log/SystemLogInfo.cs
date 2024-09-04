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

using PetaPoco;
using RsCode.Domain;
using System;

namespace RsCode
{
    /// <summary>
    /// 日志明细
    /// </summary>
    [TableName("rscode_system_log")]
    [PrimaryKey("id")]
    public class SystemLogInfo:IEntity<long>
    {
        public SystemLogInfo()
        {

        }
        /// <summary>
        /// 序列
        /// </summary>
       [Column("Id")]
        public long Id { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column("LogDate")]
        public System.DateTime LogDate { get; set; } = DateTime.Now;

        [Column("Thread")]
        public string Thread { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Column("LogLevel")]
        public string LogLevel { get; set; } = "";
       
        /// <summary>
        /// 记录器名字
        /// </summary>
        [Column("Logger")]
        public string Logger { get; set; } = "";
        /// <summary>
        /// 消息
        /// </summary>
        [Column("LogMessage")]
        public string LogMessage { get; set; } = "";
        /// <summary>
        /// 异常信息
        /// </summary>
        [Column("ExceptionData")]
        public string ExceptionData { get; set; } = "";
       
    }
}
