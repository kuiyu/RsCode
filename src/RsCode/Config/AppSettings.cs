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
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace RsCode
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
        
        public static string Get(string key, string JsonFilePath = "appsettings.json")
        {
                SetJsonFile(JsonFilePath);
            return config.GetValue<string>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public static T GetValue<T>(string Key, string JsonFilePath = "appsettings.json")
        {
           return  GetSection(Key,JsonFilePath).Get<T>(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public static T GetAllValue<T>(string JsonFilePath = "appsettings.json")
        {
            return GetSection("").Get<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key, string JsonFilePath = "appsettings.json")
        {
            SetJsonFile(JsonFilePath);
            return config.GetSection(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStrName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connStrName)
        {
            if (config == null)
            {
                SetJsonFile();
            }

            return config.GetValue<string>("ConnectionStrings:" + connStrName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public static T GetSection<T>(string key, string JsonFilePath = "appsettings.json")
        {
            return GetSection(key, JsonFilePath).Get<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public static T GetAllSection<T>(string JsonFilePath = "appsettings.json")
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
            
       /// <summary>
       /// 
       /// </summary>
       /// <param name="JsonFilePath"></param>
       /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="jsonConents"></param>
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

     
 
