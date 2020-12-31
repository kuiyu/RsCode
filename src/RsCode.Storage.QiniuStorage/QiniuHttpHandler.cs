/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //string contentType = "";
            //IEnumerable<string> s;
            //if(request.Headers.TryGetValues("Content-Type",out s))
            //{
            //    contentType = s.FirstOrDefault();
            //}
            //request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
             
            request.Headers.Add("Host", request.RequestUri.Host);

            string token =await new Signature(mac).SignRequestAsync(request); 

            request.Headers.Add("Authorization", $"QBox {token}"); 
            request.Headers.Add("User-Agent", "RsCode.Storage.QiniuStorage/1.0");  
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
