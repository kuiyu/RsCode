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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    /// <summary>
    /// cmd命令帮助类
    /// </summary>
   public class CmdHelper
    {

        /// <summary>
        /// 使用系统shell 指定命令和参数 设置标准输出
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="param">参数</param>
        public static void Run(string cmd,string param)
        {
            
                //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
                var psi = new ProcessStartInfo(cmd, param) { RedirectStandardOutput = true };
                //启动
                var proc = Process.Start(psi);
                if (proc == null)
                {
                    Console.WriteLine("Can not exec.");
                }
                else
                {
                    Console.WriteLine("-------------Start read standard output--------------");
                    //开始读取
                    using (var sr = proc.StandardOutput)
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }

                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                    Console.WriteLine("---------------Read end------------------");
                    //                Console.WriteLine($"Total execute time :{(proc.ExitTime-proc.StartTime).TotalMilliseconds} ms");
                    Console.WriteLine($"Exited Code ： {proc.ExitCode}");
                }
        }
        /// <summary>
        /// 执行指定文件中的命令
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public static void Run(string fileName)
        {
           
                //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
                var psi = new ProcessStartInfo(fileName) { RedirectStandardOutput = true };
                //启动
                var proc = Process.Start(psi);
                if (proc == null)
                {
                    Console.WriteLine("Can not exec.");
                }
                else
                {
                    Console.WriteLine("-------------Start read standard output--------------");
                    //开始读取
                    using (var sr = proc.StandardOutput)
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }

                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                    Console.WriteLine("---------------Read end------------------");
                    Console.WriteLine($"Exited Code ： {proc.ExitCode}");
                }
        }

        /// <summary>
        /// 是否是Windows系统
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// 是否是Linux系统
        /// </summary>
        /// <returns></returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
        /// <summary>
        /// 是否是OSX系统
        /// </summary>
        /// <returns></returns>
        public static bool IsOSX()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }

     
}
