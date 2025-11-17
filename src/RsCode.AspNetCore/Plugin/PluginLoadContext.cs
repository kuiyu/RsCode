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

namespace RsCode.AspNetCore.Plugin
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;
        

        public PluginLoadContext(string pluginPath) : base(isCollectible: true) { _resolver = new AssemblyDependencyResolver(pluginPath); }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // 首先检查是否为共享程序集
            if (ReferenceLoader.IsSharedAssembly(assemblyName.Name))
            {
                // 尝试从默认上下文加载共享程序集
                var sharedAssembly = AssemblyLoadContext.Default.Assemblies
                    .FirstOrDefault(a => a.GetName().Name == assemblyName.Name);
                if (sharedAssembly != null)
                {
                    return sharedAssembly;
                }
            }

            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
        //public Assembly LoadFromStreamWithTracking(Stream stream)
        //{
        //    var assembly = LoadFromStream(stream);
        //    _loadedAssemblies.Add(assembly);
        //    return assembly;
        //}

        //public new void Unload()
        //{
        //    // 清理跟踪的汇编
        //    _loadedAssemblies.Clear();
        //    base.Unload();
        //}
    }
}
