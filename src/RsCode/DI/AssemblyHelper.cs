﻿/*
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RsCode.DI
{
    internal class AssemblyHelper
    {
        private static List<Assembly> _loadedAssemblies = new List<Assembly>();

        public static List<Assembly> GetLoadedAssemblies(params string[] scanAssembliesStartsWith)
        {
            if (_loadedAssemblies.Any())
            {
                return _loadedAssemblies;
            }

            LoadAssemblies(scanAssembliesStartsWith);
            return _loadedAssemblies;
        }

        private static void LoadAssemblies(params string[] scanAssembliesStartsWith)
        {
            HashSet<Assembly> loadedAssemblies = new HashSet<Assembly>();

            List<string> assembliesToBeLoaded = new List<string>();

            string appDllsDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (scanAssembliesStartsWith?.Any() == true)
            {
                if (scanAssembliesStartsWith.Length == 1)
                {
                    string searchPattern = $"{scanAssembliesStartsWith.First()}*.dll";
                    string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                    assembliesToBeLoaded.AddRange(assemblyPaths);
                }

                if (scanAssembliesStartsWith.Length > 1)
                {
                    foreach (string starsWith in scanAssembliesStartsWith)
                    {
                        string searchPattern = $"{starsWith}*.dll";
                        string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                        assembliesToBeLoaded.AddRange(assemblyPaths);
                    }
                }
            }
            else
            {
                string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, "*.dll");
                assembliesToBeLoaded.AddRange(assemblyPaths);
            }

            foreach (string path in assembliesToBeLoaded)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(path);
                    loadedAssemblies.Add(assembly);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            _loadedAssemblies = loadedAssemblies.ToList();
        }
    }
}
