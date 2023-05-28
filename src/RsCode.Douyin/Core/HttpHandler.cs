/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace RsCode.Douyin.Core
{
    public class HttpHandler:DelegatingHandler
    {
        DouyinOptions options;
        public HttpHandler(DouyinOptions options)
        {
            this.options = options;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            InnerHandler = handler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = await BuildAuthAsync(request);
           
            request.Headers.Add("Byte-Authorization", $"SHA256-RSA2048 {auth}");
           // request.Headers.Add("Content-Type", "application/json");
             
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Unknown")));
            request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            
            return await base.SendAsync(request, cancellationToken);
        }

        protected async Task<string> BuildAuthAsync(HttpRequestMessage request)
        {
            string method = request.Method.ToString();
            string body = "";
            if (method == "POST" || method == "PUT" || method == "PATCH")
            {
                var content = request.Content;
                body = await content.ReadAsStringAsync();
            }

            string uri = request.RequestUri.PathAndQuery;
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = DouyinTool.GetNonceStr();// Path.GetRandomFileName();
            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";
            string signature =DouyinTool.Sign(message,options);

            return $"appid=\"{options.AppId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",signature=\"{signature}\"";
        }

      
    }
}
