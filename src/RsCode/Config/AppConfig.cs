using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace RsCode.Config
{
    public class AppConfig
    {
        string root;
        string jsonFilePath;
        IConfiguration config;
        static JObject CurrentJsonObject;

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
