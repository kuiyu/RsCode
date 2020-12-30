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
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage
{
    public class QiniuHttpHandler: DelegatingHandler
    {
        public QiniuHttpHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            InnerHandler = handler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            request.Headers.Add("Authorization", "");
            var data = await request.Content.ReadAsStringAsync();
            request.Headers.Add("User-Agent", "RsCode.Storage.QiniuStorage/1.0"); 
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Host","");
       
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
