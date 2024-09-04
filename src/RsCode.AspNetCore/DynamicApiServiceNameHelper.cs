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
using System.Text;
using System.Text.RegularExpressions;

namespace RsCode.AspNetCore
{
    internal static class DynamicApiServiceNameHelper
    {
        private static readonly Regex ServiceNameRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)(\/([a-zA-Z_][a-zA-Z0-9_]*))+$");
        private static readonly Regex ServiceNameWithActionRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)(\/([a-zA-Z_][a-zA-Z0-9_]*)){2,}$");

        public static bool IsValidServiceName(string serviceName)
        {
            return ServiceNameRegex.IsMatch(serviceName);
        }

        public static bool IsValidServiceNameWithAction(string serviceNameWithAction)
        {
            return ServiceNameWithActionRegex.IsMatch(serviceNameWithAction);
        }

        public static string GetServiceNameInServiceNameWithAction(string serviceNameWithAction)
        {
            return serviceNameWithAction.Substring(0, serviceNameWithAction.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase));
        }

        public static string GetActionNameInServiceNameWithAction(string serviceNameWithAction)
        {
            return serviceNameWithAction.Substring(serviceNameWithAction.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1);
        }
    }
}
