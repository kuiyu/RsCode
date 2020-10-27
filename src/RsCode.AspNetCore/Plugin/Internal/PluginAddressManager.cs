using System.Collections.Generic;
using System.Linq;

namespace RsCode.AspNetCore.Plugin.Internal
{
    internal class PluginAddressManager
    {
        internal PluginAddressManager()
        {
            Initialize();
        }

        private List<string> FilesFullAddresses { get; set; }

        private void Initialize()
        {
            FilesFullAddresses = new List<string>();
        }

        public string Get(string assemblyFileName)
        {
            return FilesFullAddresses.FirstOrDefault(s => s.EndsWith(assemblyFileName));
        }

        public void Add(string assemblyAddress)
        {
            FilesFullAddresses.Add(assemblyAddress);
        }
    }
}