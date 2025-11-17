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

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RsCode
{
    /// <summary>
    /// 读取json配置
    /// </summary>
    public static class ConfigurationHelper
    {
       
        static Dictionary<string, IConfiguration> Configurations;
        static ConfigurationHelper()
        {
            Configurations = new Dictionary<string, IConfiguration>();
            string key = "appsettings.json";
            LoadConfig(key);
        }
        static IConfiguration LoadConfig(string jsonFileName)
        {
            if (!Configurations.TryGetValue(jsonFileName, out var _config))
            {
                string key = jsonFileName;
                // 加载配置文件
                var _configuration = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile(key, optional: false, reloadOnChange: true)
                   .Build();

                Configurations.Add(key, _configuration);
                return _configuration;
            }
            else
            {
                return _config;
            }
        }

        static IConfiguration GetConfiguration(string configFile = "appsettings.json")
        {
            if(Configurations.TryGetValue(configFile,out var _configuration)==false)
            {
                _configuration = LoadConfig(configFile);
            }
            return _configuration;
        }
        public static T GetSection<T>(string sectionName,string configFile="appsettings.json") where T : new()
        {
            var _configuration = GetConfiguration(configFile);
            var section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }

        public static T Get<T>(string configFile = "appsettings.json") where T : new()
        {
            var _configuration = GetConfiguration(configFile);
            var settings = new T();
            _configuration.Bind(settings);
            return settings;
        }

        public static string GetConnectionString(string name, string configFile = "appsettings.json")
        {
            var _configuration = GetConfiguration(configFile);
            return _configuration.GetConnectionString(name);
        }

        public static string GetValue(string key, string configFile = "appsettings.json")
        {
            var _configuration = GetConfiguration(configFile);
            return _configuration[key];
        }

        static string jsonFilePath = "";
        public static JObject GetJObject(string configFile = "appsettings.json")
        {
             
            //SetJsonFile(JsonFilePath);
            jsonFilePath = Path.Combine(AppContext.BaseDirectory, configFile);
            using (StreamReader file = new StreamReader(jsonFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                return  (JToken.ReadFrom(reader) as JObject);
            }
            
        }

        public static void Save(dynamic obj)
        {
            var jsonConents = JsonConvert.SerializeObject(obj, Formatting.Indented);
            WriteJsonFile(jsonFilePath, jsonConents);
        }

        public static void WriteJsonFile(string path, string jsonConents)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(jsonConents);
                }
            }
        }
    }
}
