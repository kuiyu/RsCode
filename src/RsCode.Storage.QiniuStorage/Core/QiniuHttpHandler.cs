/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using RsCode.Threading;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage
{
    public class QiniuHttpHandler: DelegatingHandler
    {
        Mac mac;
        Auth auth;
        TokenType tokenType;
        public QiniuHttpHandler(TokenType _tokenType)
        { 
            mac = CallContext<Mac>.GetData("qiniu_option");
            HttpClientHandler handler = new HttpClientHandler();
            InnerHandler = handler;
            auth= new Auth(mac);
            tokenType = _tokenType;
        }
       

        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Host", request.RequestUri.Host);
            var requestUrl = request.RequestUri.AbsoluteUri;
            
            string token=GetToken(requestUrl );
             
            if(request.Content!=null)
            {
                string body = await request.Content.ReadAsStringAsync();
                
                var data = Encoding.UTF8.GetBytes(body);
                 
                if (request.Content.Headers.ContentType.MediaType== "application/x-www-form-urlencoded")
                {
                    
                }
                
                token = GetToken( requestUrl,data);  
            }
            request.Headers.Add("Authorization", token);
            request.Headers.Add("User-Agent", "RsCode.Storage.QiniuStorage/1.0");  
            
            
            return await base.SendAsync(request, cancellationToken);
        }

        string GetToken(string urlOrJsonstr,byte[] data=null)
        {
            if(tokenType==TokenType.Download)
            {
                return auth.CreateDownloadToken(urlOrJsonstr);
            }
            if (tokenType == TokenType.Manager)
            {
                return auth.CreateManageToken( urlOrJsonstr,data);
            }
            if(tokenType== TokenType.Upload)
            {
                return auth.CreateUploadToken(urlOrJsonstr);
            }
            if(tokenType==TokenType.StreamManage)
            {
                return auth.CreateStreamManageToken(urlOrJsonstr);
            }
            if(tokenType==TokenType.StreamPublish )
            {
                return auth.CreateStreamPublishToken(urlOrJsonstr);
            }
            throw new System.Exception("TokenType Error");
        }
       
    }
}
