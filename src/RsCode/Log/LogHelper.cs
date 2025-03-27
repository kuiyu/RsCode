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

using log4net;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RsCode
{
    /// <summary>
    /// 日志记录工具
    /// </summary>
    public static partial class LogHelper
    {

        public static void Info(string message)
        {
            try
            {
                message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + message + "\r\n";
                WriteFile(message);
            }
            catch (Exception e)
            {
                WriteFile(e.Message);
            }

        }
        static void WriteFile(string s)
        {
            try
            {
                var dir = System.IO.Path.Combine(AppContext.BaseDirectory, "logs");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var file = Path.Combine(dir, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                File.AppendAllText(file, s);
            }
            catch (Exception e)
            {

            }

        }

        public static void WriteLog(Type type,Exception ex)
        {
            WriteLog(type, ex.Message, Log4NetLevel.Error, ex);
        } 
        /// <summary>
        /// 调用Log4net写日志，日志等级为 ：错误（Error）
        /// </summary>
        /// <param name="logContent">日志内容</param>
        public static void WriteLog(string logContent)
        {
            WriteLog(null, logContent, Log4NetLevel.Error);
        }

        /// <summary>
        /// 调用Log4net写日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="log4Level">记录日志等级，枚举</param>
        public static void WriteLog(string logContent, Log4NetLevel log4Level)
        {
            WriteLog(null, logContent, log4Level);
        }

        /// <summary>
        /// 调用Log4net写日志
        /// </summary>
        /// <param name="type">类的类型，指定日志中错误的具体类。例如：typeof(Index)，Index是类名，如果为空表示不指定类</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="log4Level">记录日志等级，枚举</param>
        public static void WriteLog(Type type, string logContent, Log4NetLevel log4Level,Exception ex=null)
        {
            
            var repositories = LogManager.GetAllRepositories();
            var repository = repositories.FirstOrDefault();
            if (repository == null) return;
            
           ILog log = type == null ? LogManager.GetLogger(repository.Name, "") : LogManager.GetLogger(type);
           
            switch (log4Level)
            {
                case Log4NetLevel.Warn:
                    log.Warn(logContent);
                    break;
                case Log4NetLevel.Debug:
                    log.Debug(logContent);
                    break;
                case Log4NetLevel.Info:
                    log.Info(logContent);
                    break;
                case Log4NetLevel.Fatal:
                    log.Fatal(logContent);
                    break;
                case Log4NetLevel.Error:
                    if (ex == null) log.Error(logContent); else log.Error(logContent, ex);
                    break;
            }
        }

        
    }


    /// <summary>
    /// log4net 日志等级类型枚举
    /// </summary>
    public enum Log4NetLevel
    {
        [Description("警告信息")]
        Warn = 1,
        [Description("调试信息")]
        Debug = 2,
        [Description("一般信息")]
        Info = 3,
        [Description("严重错误")]
        Fatal = 4,
        [Description("错误日志")]
        Error = 5
    }
}

