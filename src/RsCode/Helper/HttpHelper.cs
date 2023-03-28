/*
 * RsCode
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


using PetaPoco;
using RsCode.AspNetCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace RsCode
{
    /// <summary>
    /// 针对RsCode WebApi响应做的封装
    /// </summary>
    public  class HttpHelper
    {
        HttpClient HttpClient { get; }
        public HttpHelper(HttpClient client)
        {
            HttpClient = client;
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public virtual async Task<T> GetAsync<T>(string url,string accessToken="")
         
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            HttpResponseMessage response = null;
            response = await HttpClient.SendAsync(httpRequestMessage);


            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401, "无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                JsonDocument doc = JsonDocument.Parse(s);
                var root = doc.RootElement;
                return GetValue<T>(root);
            }
            else
            {
                int code = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                GetException(code, $"RequestUri={httpRequestMessage.RequestUri.ToString()}" + content);
                return default(T);
            }

            
        }
        /// <summary>
        /// 获取PetaPoco.Page结果的数据,只适用于RsCode的固定数据格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public virtual async Task<Page<T>> GetPageAsync<T>(string url, string accessToken = "")
        {
            Page<T> page = new Page<T>();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            var response = await HttpClient.SendAsync(httpRequestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401, "无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();
                
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                JsonDocument doc = JsonDocument.Parse(s);
                var root = doc.RootElement;
                var result = root.GetProperty("result");
                var items = result.GetProperty("items"); 
                var ls = items.EnumerateArray();
                var data = JsonSerializer.Deserialize<List<T>>(ls.JsonSerialize(), options); 
                page.Items = data;
                page.TotalItems =long.Parse(result.GetProperty("totalItems").ToString());
                page.TotalPages = long.Parse(result.GetProperty("totalPages").ToString());
                page.ItemsPerPage = long.Parse(result.GetProperty("itemsPerPage").ToString());
                page.CurrentPage = long.Parse(result.GetProperty("currentPage").ToString());
            }
            else
            {
                int code = (int)response.StatusCode;
                string content =await response.Content.ReadAsStringAsync();
                GetException(code, $"RequestUri={httpRequestMessage.RequestUri.ToString()}"+ content);

            }
              return page;
        }

       /// <summary>
       /// 请求POST数据
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="url"></param>
       /// <param name="httpContent"></param>
       /// <param name="accessToken"></param>
       /// <returns></returns>
       /// <exception cref="AppException"></exception>
        public virtual async Task<T> PostAsync<T>(string url, HttpContent httpContent,string accessToken="")
            
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = httpContent;
            if(!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

                HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401,"无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string s = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                    JsonDocument doc = JsonDocument.Parse(s);
                    var root = doc.RootElement;
               
                    return GetValue<T>(root);
                }
            else
            {
                int code = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                GetException(code, $"RequestUri={response.RequestMessage.RequestUri.ToString()}" + content);

                return default(T);
            } 
        }
        
        

        /// <summary>
        /// PUT数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public virtual async Task<T> PutAsync<T>(string url, HttpContent httpContent, string accessToken = "")

        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = httpContent;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401, "无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                JsonDocument doc = JsonDocument.Parse(s);
                var root = doc.RootElement;
                return GetValue<T>(root);
            }
            else
            {
                int code = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                GetException(code, $"RequestUri={response.RequestMessage.RequestUri.ToString()}" + content);
                return default(T);
            } 

        }
        /// <summary>
        /// DELETE数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public virtual async Task<T> DeleteAsync<T>(string url, HttpContent httpContent, string accessToken = "")

        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Content = httpContent;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401, "无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                JsonDocument doc = JsonDocument.Parse(s);
                var root = doc.RootElement;
                return GetValue<T>(root);
            }
            else
            {
                int code = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                GetException(code, $"RequestUri={response.RequestMessage.RequestUri.ToString()}" + content);
                return default(T);
            }



        }

        /// <summary>
        /// 获取地址栏参数值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlParamName"></param>
        /// <returns></returns>
        public string GetUrlParamValue(string url,string urlParamName)
        {
            string query  = new Uri(url).Query;
            var p=HttpUtility.ParseQueryString(query);
            return p.Get(urlParamName);
        }

        void GetException(int code,string content)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(content))
                {
                    if(content.Contains("{")&&content.Contains("}"))
                    {
                        var returnInfo = Deserializer<ReturnInfo>(content);
                        if (returnInfo != null)
                        {
 throw new AppException(returnInfo.code, returnInfo.Msg);
                        }else
                        {
                            throw new AppException(code, content);
                        }
                   
                    }
                  else
                    {
                        throw new AppException(code, content);
                    }
                }
                else
                {
                    throw new AppException(code, content);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        T Deserializer<T>(string json)
        {
            try
            {
               return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        T GetValue<T>(JsonElement root)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverterUsingDateTimeParse());

            JsonElement result;
            var ret = root.TryGetProperty("result", out result);
            if (ret)
            {
                var data = JsonSerializer.Deserialize<T>(result.JsonSerialize(), options);
                return data;
            }
            else
            {
                string s = root.JsonSerialize();
                var data = JsonSerializer.Deserialize<T>(s, options); ;
                return data;
            }
        }
    }
    /// <summary>
    /// 日期格式处理
    /// </summary>

    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public DateTimeConverterUsingDateTimeParse(string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            _DateTimeFormat = DateTimeFormat;
        }
        string _DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(this._DateTimeFormat));
        }
    }

}
