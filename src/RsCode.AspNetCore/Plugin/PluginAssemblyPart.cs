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

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

namespace RsCode.AspNetCore.Plugin
{
    public class PluginAssemblyPart : AssemblyPart, ICompilationReferencesProvider
    {
        public PluginAssemblyPart(Assembly assembly) : base(assembly) { }

        public IEnumerable<string> GetReferencePaths() => Array.Empty<string>();
    }

}
