using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using RsCode.AspNetCore.Plugin.Abstract;

namespace RsCode.AspNetCore.Plugin
{
    public static class PluginManager
    {
        static PluginManager()
        {
            ResetModules();
        }

        internal static Dictionary<IPluginModule, Assembly> Modules { get; set; }

        public static IEnumerable<IPluginModule> GetModules()
        {
            return Modules.Select(m => m.Key).ToList();
        }

        public static IPluginModule GetModule(string name)
        {
            return GetModules().FirstOrDefault(m => m.Name == name);
        }

        internal static void ResetModules()
        {
            Modules = new Dictionary<IPluginModule, Assembly>();
        }
    }
}