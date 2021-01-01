/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage
{
    public class QiniuHttpHandler: DelegatingHandler
    {
        Mac mac;
        public QiniuHttpHandler(Mac _mac)
        {
            mac = _mac;
            HttpClientHandler handler = new HttpClientHandler();
            InnerHandler = handler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Host", request.RequestUri.Host);
            var requestUrl = request.RequestUri.AbsoluteUri;
            
           string token=Auth.CreateManageToken(mac,requestUrl );
            if(request.Content!=null)
            {
                var body = await request.Content.ReadAsByteArrayAsync();
                token = Auth.CreateManageToken(mac, requestUrl,body);
            }
            request.Headers.Add("Authorization", token);
            request.Headers.Add("User-Agent", "RsCode.Storage.QiniuStorage/1.0");  
            
            return await base.SendAsync(request, cancellationToken);
        }

       
    }
}
