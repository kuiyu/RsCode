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
using System.Reflection;
using System.Runtime.Loader;

namespace System
{
    public class HostAssemblyLoadContext : AssemblyLoadContext
    {
        // Resolver of the locations of the assemblies that are dependencies of the
        // main plugin assembly.
        // private AssemblyDependencyResolver _resolver;

        //public HostAssemblyLoadContext(string pluginPath) : base(isCollectible: true)
        //{
        // //   _resolver = new AssemblyDependencyResolver(pluginPath);
        //}


        protected override Assembly Load(AssemblyName name)
        {
            //string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            //if (assemblyPath != null)
            //{
            //    Console.WriteLine($"Loading assembly {assemblyPath} into the HostAssemblyLoadContext");
            //    return LoadFromAssemblyPath(assemblyPath);
            //}

            return null;
        }

    }
}
