using System.Reflection;
using System.Security.Cryptography.Xml;


namespace RsCode.AspNetCore.Plugin
{

    public class ReferenceLoader
    {
        public static void LoadStreamsIntoContext(PluginLoadContext pluginContext, Assembly pluginAssembly, string pluginPath)
        {
            var references = pluginAssembly.GetReferencedAssemblies();

            foreach (var reference in references)
            {
                var name = reference.Name;
                var version = reference.Version?.ToString() ?? "";
                // 如果是共享程序集，跳过加载
                if (IsSharedAssembly(name))
                {
                    continue;
                }


               string path = Path.Combine(pluginPath, $"{name}.dll");
                if (!File.Exists(path))
                    continue;

                var stream = GetStream(name, version);
                if (stream != null)
                {
                    pluginContext.LoadFromStream(stream);
                    continue;
                }

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var referenceAssembly = pluginContext.LoadFromStream(fs);

                    var memoryStream = new MemoryStream();

                    fs.Position = 0;
                    fs.CopyTo(memoryStream);
                    fs.Position = 0;
                    memoryStream.Position = 0;
                    SaveStream(name, version, memoryStream);

                    LoadStreamsIntoContext(pluginContext, referenceAssembly, pluginPath);

                    pluginContext.LoadFromStream(memoryStream);
                }

            }
        }
        public static void AddSharedAssembly(string[] sharedAssemblyNames)
        {
            _sharedAssemblyNames = sharedAssemblyNames;
        }
        private static Dictionary<CachedReferenceItemKey, Stream> _cachedReferences = new Dictionary<CachedReferenceItemKey, Stream>();
        static Stream GetStream(string name, string version)
        {
            var key = _cachedReferences.Keys.FirstOrDefault(p => p.ReferenceName == name
                && p.Version == version);

            if (key != null)
            {
                _cachedReferences[key].Position = 0;
                return _cachedReferences[key];
            }

            return null;
        }
        static void SaveStream(string name, string version, Stream stream)
        {
            if (Exist(name, version))
            {
                return;
            }


            _cachedReferences.Add(new CachedReferenceItemKey { ReferenceName = name, Version = version }, stream);
        }
        static bool Exist(string name, string version)
        {
            return _cachedReferences.Keys.Any(p => p.ReferenceName == name
                && p.Version == version);
        }
        public static bool IsSharedAssembly(string name)
        {
            if (_sharedAssemblyNames != null)
            {
                if (_sharedAssemblyNames.Contains(name))
                    return true;
            }

            if (name.StartsWith("Microsoft.")
                || name.StartsWith("System.")
                || name.StartsWith("api-ms-win-core")
                || name.StartsWith("clrcompression")
                || name.StartsWith("clretwrc")
                || name.StartsWith("clrjit")
                || name.StartsWith("coreclr")
                || name.StartsWith("hostpolicy")
                || name.StartsWith("mscordaccore")
                || name.StartsWith("mscor")
                || name.StartsWith("netstandard")
                ||name.StartsWith("mscorlib")
                || name.StartsWith("ucrtbase")
                || name.StartsWith("WindowsBase")
                || name.StartsWith("aspnetcorev2_inprocess")
                || name == "RsCode"
                || name.StartsWith("RsCode.AspNetCore")

                )
            {
                return true;
            }

            // 增强：检查当前已加载的程序集
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (loadedAssemblies.Any(a => a.GetName().Name == name))
                return true;

            return false;

        }
        /// <summary>
        /// 主项目与插件项目共用的程序集名称
        /// </summary>
        static string[] _sharedAssemblyNames = new string[] { };
    }

 
    
    
    public class CachedReferenceItemKey
    {
        public string ReferenceName { get; set; }

        public string Version { get; set; }
    }




 

}
