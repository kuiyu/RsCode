/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json;

namespace RsCode.Douyin.Core
{
    public class DouyinHttpClient
    {
        public HttpClient httpClient { get; private set; }

        public DouyinHttpClient(HttpClient client)
        {
            httpClient = client;
        }

        public void LoadHandler(HttpHandler httpHandler)
        {
            httpClient = new HttpClient(httpHandler);
            httpClient.BaseAddress = new Uri("https://open.douyin.com");
        }

        public async Task<T> GetAsync<T>(string url)
            where T : DouyinResponse
        {
            using (var response = await httpClient.GetAsync(url))
            {
                int statusCode = Convert.ToInt32(response.StatusCode);
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var res = await response.Content.ReadAsStringAsync();
                  
                    return await JsonSerializer.DeserializeAsync<T>(responseStream);
                }
            }

        }


        public async Task<T> PostAsync<T>(string url, HttpContent httpContent)
            where T : DouyinResponse
        {
            using (httpContent)
            using (var response = await httpClient.PostAsync(url, httpContent))
            {
                var res = await response.Content.ReadAsStringAsync(); 
                return JsonSerializer.Deserialize<T>(res);
            }

        }
    }
}
