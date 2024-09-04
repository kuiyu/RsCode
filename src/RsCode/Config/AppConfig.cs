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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace RsCode.Config
{/// <summary>
/// 
/// </summary>
    public class AppConfig
    {
        string root;
        string jsonFilePath;
        IConfiguration config;
        static JObject CurrentJsonObject;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AppConfig(IConfiguration config)
        {
            root = AppContext.BaseDirectory;
            jsonFilePath = "appsettings.json";
        }

        public string Get(string Key, string JsonFilePath = "appsettings.json")
        {

            SetJsonFile(JsonFilePath);
            return config.GetValue<string>(Key);
        }

        public T GetValue<T>(string Key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(Key, JsonFilePath).Get<T>();
        }

        public T GetValue<T>(string JsonFilePath = "appsettings.json")
        {
            return GetSection("").Get<T>();
        }

        public IConfigurationSection GetSection(string key, string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.GetSection(key);
        }
        public string GetConnectionString(string connStrName)
        {
            if (config == null)
            {
                SetJsonFile();
            }

            return config.GetValue<string>("ConnectionStrings:" + connStrName);
        }

        public T GetSection<T>(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Get<T>();
        }

        public T GetSection<T>(string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.Get<T>();
        }

        /// <summary>
        /// 检查节点是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public bool Exists(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Exists();
        }


        public JObject GetJObject(string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            using (StreamReader file = new StreamReader(jsonFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                CurrentJsonObject = (JToken.ReadFrom(reader) as JObject);
            }
            return CurrentJsonObject;
        }

        public void Save(dynamic obj)
        {
            var jsonConents = JsonConvert.SerializeObject(obj, Formatting.Indented);
            WriteJsonFile(jsonFilePath, jsonConents);
        }

        IConfiguration SetJsonFile(string JsonFile = "appsettings.json")
        {

            string jsonFileFullPath = Path.Combine(root, JsonFile);
            if (!File.Exists(jsonFileFullPath))
                throw new ArgumentException("not find " + JsonFile);

            if (jsonFilePath != jsonFileFullPath)
            {
                config = new ConfigurationBuilder()
                        .AddJsonFile(jsonFileFullPath, optional: true, reloadOnChange: true)
                        .Build();
                //ChangeToken.OnChange(() => config.GetReloadToken(), () => {

                //});
                jsonFilePath = jsonFileFullPath;
            }
            return config;
        }


        public void WriteJsonFile(string path, string jsonConents)
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
