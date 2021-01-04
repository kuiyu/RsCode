/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage
{
    public class QiniuHttpClient
    {
        HttpClient Client{ get; set; }

        public QiniuHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
        public void LoadHandler(QiniuHttpHandler handler)
        {            
            Client = new HttpClient(handler);
            
        }
        public async Task<HttpResponseMessage> GetAsync(string url) 
        {
            return await Client.GetAsync(url); 
        }
        public async Task<T> GetAsync<T>(string url)
           where T : StorageResponse
        {
         
            using (var response = await GetAsync(url))
            {
                int statusCode = Convert.ToInt32(response.StatusCode);
                if (statusCode == 200)
                {
                    if(response.Content!=null)
                    {
                        var s = await response.Content.ReadAsStringAsync();
                         return  JsonSerializer.Deserialize<T>(s);
                    }else
                    {
                        StorageResponse storageResponse = new StorageResponse();
                        storageResponse.HttpCode = statusCode;
                        return storageResponse as T;
                    }
                     
                  
                }
                else
                {
                    var s = await response.Content.ReadAsStringAsync();
                    var t = JsonSerializer.Deserialize<T>(s);
                    t.HttpCode = statusCode;
                    return t;
                }

            }

        }
        public async Task<T> PostAsync<T>(string url,HttpContent httpContent)
            where T:StorageResponse
        {
            using (httpContent)
            using (var response = await Client.PostAsync(url, httpContent))
            {
                int statusCode = Convert.ToInt32(response.StatusCode);
                if (statusCode == 200)
                {
                    if (response.Content != null)
                    {
                        var s = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<T>(s);
                    }
                    else
                    {
                        StorageResponse storageResponse = new StorageResponse();
                        storageResponse.HttpCode = statusCode;
                        return storageResponse as T;
                    }
                }
                else
                {
                    var s = await response.Content.ReadAsStringAsync();
                    var t = JsonSerializer.Deserialize<T>(s);
                    t.HttpCode = statusCode;   
                    return t;
                }

            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent) 
        {
            using (httpContent)
            using (var response = await Client.PostAsync(url, httpContent))
            {
                return response; 
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        { 
            using (var response = await Client.DeleteAsync(url))
            {
                return response;
            }
        }
    }
}
