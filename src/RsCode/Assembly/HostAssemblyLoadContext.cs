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
