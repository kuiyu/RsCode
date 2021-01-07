/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RsCode.Storage.LocalStorage
{
    public class LocalStorageService : IStorageProvider
    {
        LocalOptions Options;
        IHttpContextAccessor httpContext;
        public LocalStorageService(IHttpContextAccessor _httpContext,IOptionsSnapshot<LocalOptions> options)
        {
            httpContext = _httpContext;
            Options = options.Value;
        }
        public string StorageName => "local";

        public async Task<UploadResult> UploadAsync()
        {
            UploadResult result = new UploadResult();
            var request = httpContext.HttpContext.Request.Form;
            string key = httpContext.HttpContext.Request.Form["key"];
            var token = httpContext.HttpContext.Request.Form["token"];
            var files = httpContext.HttpContext.Request.Form.Files;

            long size = files.Sum(f => f.Length); //统计所有文件的大小

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");  //存储文件的路径           

            foreach (var item in files)     //上传选定的文件列表
            {
                if (item.Length > 0)        //文件大小 0 才上传
                {
                    var savePath = filepath + "\\" + key.Replace("/", "\\");     //当前上传文件应存放的位置
                    if (System.IO.File.Exists(savePath) == true)        //如果文件已经存在,跳过此文件的上传
                    {
                        //文件已存在
                        continue;
                    }

                    //上传文件
                    using (var stream = new FileStream(savePath, FileMode.Create))      //创建特定名称的文件流
                    {
                        try
                        {
                            await item.CopyToAsync(stream);     //上传文件
                            result.Res = "ok";
                            result.Key = key;
                        }
                        catch (Exception ex)        //上传异常处理
                        {
                            result.Res = ex.Message;
                        }
                    }
                }
            }
            return result;
        }
        
        public TokenResult GetUploadToken(bool isClient = true)
        {
            var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            return new TokenResult
            {
                Domain = url,
                Token = "",
                UploadUrl =Options.UploadUrl
            };
        }

        public TokenResult GetDownloadToken(bool isClient = true)
        {
            var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            return new TokenResult
            {
                Domain = url,
                Token = "",
                UploadUrl = Options.UploadUrl
            };
        }

        public TokenResult GetManageToken(bool isClient = true)
        {
          var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            return new TokenResult
            {
                Domain = url,
                Token = "",
                UploadUrl = Options.UploadUrl
            };
        }

        public Task<T> SendAsync<T>(StorageRequest request) where T : StorageResponse
        {
            throw new NotImplementedException();
        }

        public string CreateDownloadUrl(string domain, string key, int expireInSeconds = 1800)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SendAsync(StorageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
