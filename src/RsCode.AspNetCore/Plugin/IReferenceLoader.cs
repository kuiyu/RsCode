using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection;


namespace RsCode.AspNetCore.Plugin
{

    public class ReferenceLoader
    {
        public static void LoadStreamsIntoContext(PluginLoadContext pluginLoadContent, Assembly pluginAssembly,string pluginPath)
        {
            var references = pluginAssembly.GetReferencedAssemblies();

            foreach (var item in references)
            {
                var name = item.Name;
                if (IsSharedFreamwork(name))
                    continue;

                var version = item.Version?.ToString() ?? "";
                string path = Path.Combine(pluginPath, $"{name}.dll");
                if (!File.Exists(path))
                    continue;

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var referenceAssembly = pluginLoadContent.LoadFromStream(fs);

                    var memoryStream = new MemoryStream();

                    fs.Position = 0;
                    fs.CopyTo(memoryStream);
                    fs.Position = 0;
                    memoryStream.Position = 0;
                    SaveStream(name, version, memoryStream);

                    LoadStreamsIntoContext(pluginLoadContent, referenceAssembly, pluginPath);
                }

            }
        }

        private static Dictionary<CachedReferenceItemKey, Stream> _cachedReferences = new Dictionary<CachedReferenceItemKey, Stream>();
        static Stream GetStream(string name, string version,string dllPath)
        {
            using (var fs=new FileStream(dllPath,FileMode.Open))
            {

            }
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
         static  bool Exist(string name, string version)
        {
            return _cachedReferences.Keys.Any(p => p.ReferenceName == name
                && p.Version == version);
        }
        private static bool IsSharedFreamwork(string name)
        {
            if (name.StartsWith("Microsoft")
                || name.StartsWith("System")
                || name.StartsWith("api-ms-win-core")
                || name.StartsWith("clrcompression")
                || name.StartsWith("clretwrc")
                || name.StartsWith("clrjit")
                || name.StartsWith("coreclr")
                || name.StartsWith("hostpolicy")
                || name.StartsWith("mscordaccore")
                || name.StartsWith("mscor")
                || name.StartsWith("netstandard")
                || name.StartsWith("ucrtbase")
                || name.StartsWith("WindowsBase")
                || name.StartsWith("aspnetcorev2_inprocess")
                || name.StartsWith("RsCode.dll")
                || name.StartsWith("RsCode.AspNetCore")

                )
            {
                return true;
            }
            return false;

        }
    }


    public interface IReferenceLoader
    {
        public void LoadStreamsIntoContext(PluginLoadContext context, string moduleFolder, Assembly assembly);
    }

    
    
    public class CachedReferenceItemKey
    {
        public string ReferenceName { get; set; }

        public string Version { get; set; }
    }




 

}
