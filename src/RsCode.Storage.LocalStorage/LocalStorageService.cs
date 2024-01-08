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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.LocalStorage
{
    public class LocalStorageService : IStorageProvider
    {
        LocalStorageOptions Options;
        IHttpContextAccessor httpContext;
        public LocalStorageService(IHttpContextAccessor _httpContext,IOptionsSnapshot<LocalStorageOptions> options)
        {
            httpContext = _httpContext;
            Options = options.Value;
        }
        public string StorageName => "local";

        public virtual async Task<UploadResult> UploadAsync()
        {
            UploadResult result = new UploadResult();
            List<string> urls = new List<string>();
            List<string> keys = new List<string>();
            var form =httpContext.HttpContext.Request.Form;
            foreach (var item in form.Files)
            {
                var ext = item.FileName.Split('.')[1];
                var fileName = $"{Guid.NewGuid().ToString("N")}.{ext}";
                var savePath = Options.SavePath;
                string filePath = Path.Combine(savePath, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                var tmp = fileName.Replace("\\", "/");
                var domainUrl = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
                urls.Add($"{domainUrl}{Options.AccessPath}/{tmp}");
                keys.Add($"{Options.AccessPath}/{tmp}");
            }
            if (form.Files.Count == 1)
            {
                result.Res = urls.FirstOrDefault();
                result.Key = keys.FirstOrDefault();
            }
            else
            {
              result.Res = JsonSerializer.Serialize(urls);
              result.Key = JsonSerializer.Serialize(keys);
            }
           
            return result;
        }
        
        public TokenResult GetUploadToken(bool isClient = true)
        {
            var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            return new TokenResult
            {
                Domain = url,
                Token = Guid.NewGuid().ToString("N"),
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

        public Task<(HttpResponseMessage,string)> SendAsync(StorageRequest request)
        {
            throw new NotImplementedException();
        }

        public string GetUploadToken(string key, DateTime expiresTime)
        {
            throw new NotImplementedException();
        }

        public string CreateDownloadUrl(string url, int expireInSeconds = 3600)
        {
            string requestFile = url;
            requestFile = requestFile.Replace("/res/", "");
            requestFile = requestFile.Replace("//", "\\");
            string physicsPath =Options.SavePath;
            string file = Path.Combine($"{physicsPath}{requestFile}");
            file = System.Web.HttpUtility.UrlDecode(file);
            if (File.Exists(file))
            {
                return file;
            }
            else
            {
               return "";
            }
        }

        public async Task<TokenResult> GetUploadTokenInfoAsync(string key, DateTime expiresTime)
        {
            var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            return new TokenResult
            {
                Domain = url,
                Token = Guid.NewGuid().ToString("N"),
                UploadUrl = Options.UploadUrl
            };
        }

        public StorageOptions UseBucket(string bucket)
        {
            throw new NotImplementedException();
        }

		public Task<UploadResult> UploadAsync(Stream stream, string key, string token)
		{
			throw new NotImplementedException();
		}
	}
}
