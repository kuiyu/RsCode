using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
    public  class AssemblyManager
    {
       [MethodImpl(MethodImplOptions.NoInlining)]
       public Assembly LoadFromAssemblyPath(string assemblyPath)
        {
            return null;
            //var alc = new HostAssemblyLoadContext(assemblyPath);
            //Assembly assembly = alc.LoadFromAssemblyPath(assemblyPath);
            //AssemblyContainer.Add(assemblyPath, alc);
            //return assembly;
        }
        //[MethodImpl(MethodImplOptions.NoInlining)]
        public void UnLoadFromAssemblyPath(string assemblyPath)
        {            
            AssemblyContainer.Remove(assemblyPath);
            
        }

    }
}
