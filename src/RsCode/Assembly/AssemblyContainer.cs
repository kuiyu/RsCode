using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    /// 程序集容器
    /// </summary>
    public static class AssemblyContainer
    {
        static Dictionary<string, HostAssemblyLoadContext> AssemblyList = null;
        static AssemblyContainer()
        {
            AssemblyList = new Dictionary<string, HostAssemblyLoadContext>();
        }
        public static void Add(string assemblyPath,HostAssemblyLoadContext assembly)
        {
            var a=Get(assemblyPath);
            if(a==null)
            {
                AssemblyList.Add(assemblyPath, assembly);
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyPath"></param>
        public static void Remove(string assemblyPath)
        {
            if(AssemblyList.ContainsKey(assemblyPath))
            {
                var assembly = Get(assemblyPath);
                //assembly.Unload();
                assembly.Unloading += Assembly_Unloading;
                AssemblyList.Remove(assemblyPath);
            }
        }

        private static void Assembly_Unloading(Runtime.Loader.AssemblyLoadContext obj)
        {
               
        }

        public static HostAssemblyLoadContext Get(string assemblyPath)
        {
            if(AssemblyList.ContainsKey(assemblyPath))
            {
                return AssemblyList[assemblyPath];
            }
            return null; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, HostAssemblyLoadContext> GetAll()
        {
            return AssemblyList;
        }



    }
}
