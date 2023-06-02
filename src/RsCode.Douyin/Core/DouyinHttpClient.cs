/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace RsCode.Douyin.Core
{
    public class DouyinHttpClient
    {
        public HttpClient httpClient { get; private set; }
        ILogger log;
        public DouyinHttpClient(HttpClient client, ILogger<DouyinHttpClient> logger)
        {
            httpClient = client;
            log = logger;
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

                try
                {
                    return JsonSerializer.Deserialize<T>(res);
                }
                catch (Exception e)
                {
                  
                    var req = httpContent.JsonSerialize();
                    log.LogError("request " + req);
                    log.LogError("response " + res);
                    log.LogError(e.Message);
                    throw e;
                }

            }

        }
    }
}
