/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace RsCode.Douyin.Core
{
    public class DouyinClient : IDouyinClient
    {

        List<DouyinOptions> options;
        DouyinHttpClient httpClient;
        IHttpContextAccessor httpContextAccessor;
        DouyinHttpClientHandler DouyinHttpClientHandler;
        ILogger log;
        public DouyinClient(DouyinHttpClientHandler douyinHttpClientHandler, IOptionsSnapshot<List<DouyinOptions>> options, DouyinHttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<DouyinClient> logger)
        {
            this.options = options.Value;
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            DouyinHttpClientHandler= douyinHttpClientHandler;
            log = logger;
        }
        DouyinOptions clientOptions { get; set; }
        public DouyinOptions UseAppId(string appId)
        {
            clientOptions = options.FirstOrDefault(o => o.AppId == appId);
            if (clientOptions == null)
                throw new Exception($"配置有误,未找到appid={appId}配置");

            var httpHandler = new HttpHandler(clientOptions);
            httpClient.LoadHandler(httpHandler);



            return clientOptions;
        }

        public async Task<T> SendAsync<T>(DouyinRequest request) where T : DouyinResponse
        {
            try
            {
                if (clientOptions == null)
                {
                    throw new Exception("未调用UseAppId()方法");
                }

                var url = request.GetApiUrl();
                var method = request.RequestMethod().ToUpper();

                if (method == "GET")
                {
                    HttpRequestMessage httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url)
                    };

                    var res = await httpClient.GetAsync<T>(url);
                    return res;
                }

                if (method == "POST")
                {
                    var option = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    };
                    string json = JsonSerializer.Serialize(request, request.GetType(), option);
                    HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var res = await httpClient.PostAsync<T>(url, httpContent);
                    return res;
                }
                throw new Exception("未定义的请求方式");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken cancellationToken)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri
            };
            return DouyinHttpClientHandler.SendRequestAsync(httpRequest, cancellationToken);
        }

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri == null||string.IsNullOrWhiteSpace(request.RequestUri.OriginalString))
            {
                throw new ArgumentException("RequestUri");
            }
            return SendAsync(request, cancellationToken);
        }
       

        public string GetIp()
        {
            var httpContext = httpContextAccessor;
            var ip = httpContext.HttpContext?.Request.Headers["X-Real-IP"].FirstOrDefault() ?? httpContext.HttpContext?.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return string.IsNullOrWhiteSpace(ip) ? "0.0.0.0" : ip;
        }
    }
}