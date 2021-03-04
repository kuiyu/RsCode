/*
 * .netcore 对json文件操作的类
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace RsCode.Config
{


    /// <summary>
    /// .netcore json config
    /// </summary>
    public class AppSettings
    {

        static string root = AppContext.BaseDirectory;
        static string jsonFilePath = "appsettings.json";
        static IConfiguration config;
        static JObject CurrentJsonObject; 
        
        public static string Get(string Key, string JsonFilePath = "appsettings.json")
        {
            
                SetJsonFile(JsonFilePath);
            return config.GetValue<string>(Key);
        }

        public static T GetValue<T>(string Key, string JsonFilePath = "appsettings.json")
        {
           return  GetSection(Key,JsonFilePath).Get<T>(); 
        }

        public static T GetValue<T>( string JsonFilePath = "appsettings.json")
        {
            return GetSection("").Get<T>(); 
        }

        public static IConfigurationSection GetSection(string key, string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.GetSection(key);
        }
        public static string GetConnectionString(string connStrName)
        {
            if (config == null)
            {
                SetJsonFile();
            }

            return config.GetValue<string>("ConnectionStrings:" + connStrName);
        }

        public static T GetSection<T>(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Get<T>();
        }

        public static T GetSection<T>(string JsonFilePath = "appsettings.json")
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
        public static bool Exists(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Exists();
        }
            
                            
       public static JObject GetJObject(string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            using (StreamReader file = new StreamReader(jsonFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
              CurrentJsonObject =  (JToken.ReadFrom(reader) as JObject); 
            }
            return CurrentJsonObject;
        }

        public static void Save(dynamic obj)
        {
            var jsonConents = JsonConvert.SerializeObject(obj, Formatting.Indented);
            WriteJsonFile(jsonFilePath, jsonConents); 
        }

        static IConfiguration SetJsonFile(string JsonFile = "appsettings.json")
        {
             
            string jsonFileFullPath = Path.Combine(root, JsonFile);
            if (!File.Exists(jsonFileFullPath))
                throw new ArgumentException("not find " + jsonFileFullPath);

            if (jsonFilePath != jsonFileFullPath)
            {
                config = new ConfigurationBuilder()
                        .AddJsonFile(jsonFileFullPath, optional: true, reloadOnChange: true)
                        .Build();
                ChangeToken.OnChange(() => config.GetReloadToken(), () => { 
                
                });
                jsonFilePath = jsonFileFullPath;
            }
            return config;
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

    
 

    public class AppSettings<T> where T:class
    {

        static string root = AppContext.BaseDirectory;
        static string jsonFilePath = "appsettings.json";
        static IConfiguration config;
        static JObject CurrentJsonObject;

        public static string Get(string Key, string JsonFilePath = "appsettings.json")
        {
            
            SetJsonFile(JsonFilePath);
            return config.GetValue<string>(Key);
        }

        public static T GetValue<T>(string Key, string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.GetValue<T>(Key);
        }



        public static IConfigurationSection GetSection(string key, string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.GetSection(key);
        }
        public static string GetConnectionString(string connStrName)
        {
            if (config == null)
            {
                SetJsonFile();
            }

            return config.GetValue<string>("ConnectionStrings:" + connStrName);
        }

        public static T GetSection<T>(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Get<T>();
        }

        public static T GetSection<T>(string JsonFilePath = "appsettings.json")
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
        public static bool Exists(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Exists();
        }


        public static JObject GetJObject(string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            using (StreamReader file = new StreamReader(jsonFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                CurrentJsonObject = (JToken.ReadFrom(reader) as JObject);
            }
            return CurrentJsonObject;
        }

        public static void Save(dynamic obj)
        {
            var jsonConents = JsonConvert.SerializeObject(obj, Formatting.Indented);
            WriteJsonFile(jsonFilePath, jsonConents);
        }

        public static void SaveJObject(dynamic obj)
        {
            var jsonConents = System.Text.Json.JsonSerializer.Serialize(obj, Formatting.Indented);// JsonConvert.SerializeObject(obj, Formatting.Indented);
            
            WriteJsonFile(jsonFilePath, jsonConents);
        }

        static IConfiguration SetJsonFile(string JsonFile = "appsettings.json")
        {

            string jsonFileFullPath = Path.Combine(root, JsonFile);
            if (!File.Exists(jsonFileFullPath))
                throw new ArgumentException("not find " + JsonFile);

            if (jsonFilePath != jsonFileFullPath)
            {
                config = new ConfigurationBuilder()
                        .AddJsonFile(jsonFileFullPath, optional: true, reloadOnChange: true)
                        .Build();
                ChangeToken.OnChange(() => config.GetReloadToken(), () => {

                });
                jsonFilePath = jsonFileFullPath;
            }
            return config;
        }


        public static void WriteJsonFile(string path, string jsonConents)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(jsonConents);
                }
            }

        }


    }

}

     
 
