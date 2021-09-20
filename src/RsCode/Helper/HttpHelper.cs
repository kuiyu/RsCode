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


using RsCode.AspNetCore;
using System;
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


        public virtual async Task<T> GetAsync<T>(string url,string accessToken="")
          where T : class
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            var response = await HttpClient.SendAsync(httpRequestMessage);
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
                var result = root.GetProperty("result");

                var data = JsonSerializer.Deserialize<T>(result.JsonSerialize(), options);
                return data;
            }
            else
            {
                string ret = await response.Content.ReadAsStringAsync();
                ReturnInfo returnInfo = JsonSerializer.Deserialize<ReturnInfo>(ret);
                if (returnInfo != null)
                {
                    throw new AppException(returnInfo.Msg);
                }
                return default(T);
            }
        }


        public virtual async Task<T> GetItemsAsync<T>(string url,string accessToken="")
           where T : class
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            var response = await HttpClient.SendAsync(httpRequestMessage); 
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new AppException(401,"无访问权限");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<ReturnInfo>(s);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                JsonDocument doc = JsonDocument.Parse(s);
                var root = doc.RootElement;
                var result = root.GetProperty("result");
                var items = result.GetProperty("items");
                var ls = items.EnumerateArray();
                var data = JsonSerializer.Deserialize<T>(ls.JsonSerialize(), options); 

                return data;
            }
            else
            {
                string ret = await response.Content.ReadAsStringAsync();
                ReturnInfo returnInfo = JsonSerializer.Deserialize<ReturnInfo>(ret);
                if (returnInfo != null)
                {
                    throw new AppException(returnInfo.Msg);
                }
                return default(T);
            }

        }
        public virtual async Task<T> PostAsync<T>(string url, HttpContent httpContent,string accessToken="")
             where T : class
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
                    var result = root.GetProperty("result");
                    var data = JsonSerializer.Deserialize<T>(result.JsonSerialize(), options);

                    return data;
                }
            else
            {
                string ret = await response.Content.ReadAsStringAsync();
                ReturnInfo returnInfo = JsonSerializer.Deserialize<ReturnInfo>(ret);
                if (returnInfo != null)
                {
                    throw new AppException(returnInfo.Msg);
                }
                return default(T);
            }



        }
        public virtual async Task<T> PostItemsAsync<T>(string url, HttpContent httpContent,string accessToken="")
            where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = httpContent;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            var response = await  HttpClient.SendAsync(request);
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
                var result = root.GetProperty("result");
                var items = result.GetProperty("items");
                var ls = items.EnumerateArray();
                var data = JsonSerializer.Deserialize<T>(ls.JsonSerialize(), options);

                return data;
            }
            else
            {
                string ret = await response.Content.ReadAsStringAsync();
                ReturnInfo returnInfo = JsonSerializer.Deserialize<ReturnInfo>(ret);
                if (returnInfo != null)
                {
                    throw new AppException(returnInfo.Msg);
                }
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

        
    }

    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
