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
 */
using System.Reflection;
using System.Runtime.Loader;

namespace RsCode.AspNetCore.Plugin
{
    public class PluginAssemblyLoadContext : AssemblyLoadContext
    {
        public PluginAssemblyLoadContext() : base(isCollectible: true)
        {

        }
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}
