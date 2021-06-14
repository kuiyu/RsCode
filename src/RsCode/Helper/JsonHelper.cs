/*
 * 项目：.net开发基础工具类
 * 作者：河南软商网络科技有限公司
 * * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Text.Json
{
    public static class JsonHelper
    {
        /// <summary>
        /// 使用json格式序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object obj)
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreNullValues = true,
                WriteIndented = true,
                AllowTrailingCommas = true
            };
            options.WriteIndented = true;
            return JsonSerializer.Serialize(obj, options);
        }
        /// <summary>
        /// 保存json对象到文件
        /// </summary>
        /// <typeparam name="T">json对象</typeparam>
        /// <param name="path">json文件路径</param>
        /// <param name="obj">json对象</param>
        /// <param name="clear">是否清空原文件</param>
        public static void Save<T>(string path, T obj, bool clear = false) where T : class
        {
            dynamic jObject;

            if (!clear)
            {
                //读取json文件内容
                using (StreamReader sr = new StreamReader(path))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    jObject = JToken.ReadFrom(reader) as JObject;
                }
            }
            else
            {
                jObject = JToken.FromObject(obj);
            }


            //动态赋值
            jObject.Tencent.WeChat = JObject.FromObject(obj);
            //序列化原内容
            var jsonContent = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            //保存
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine(jsonContent);
            }
        }
        /// <summary>
        /// 获取指key的值
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }
        
        public static T ToObject<T>(this JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
        public static T ToObject<T>(this JsonDocument document)
        {
            var json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
         
   
    }
}
