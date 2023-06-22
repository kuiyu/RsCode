/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.Extensions.Options;

using RsCode.Storage.QiniuStorage;
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RsCode.Storage
{
    public class QiniuStorageProvider : IStorageProvider
    {
       
        List<StorageOptions> storageOptions;
        Mac mac;
        StorageOptions storageOption;
         QiniuHttpClient httpClient;
         ZoneHelper zoneHelper;
        public QiniuStorageProvider(
            IOptionsSnapshot<List<StorageOptions>> _options, 
            QiniuHttpClient _qiniuHttpClient ,
            ZoneHelper _zoneHelper
            )
        {
            httpClient = _qiniuHttpClient;
            storageOptions = _options.Value; 
            zoneHelper = _zoneHelper;
            
        }
        public string StorageName { get; } = "qiniu";

        public StorageOptions UseBucket(string bucket)
        {
            storageOption = storageOptions.FirstOrDefault(c=>c.Bucket==bucket&&c.Name==StorageName);
            mac = new Mac(storageOption.AccessKey, storageOption.SecretKey);
            CallContext<Mac>.SetData("qiniu_option", mac);
            return storageOption;
        }
        public async Task<(HttpResponseMessage, string)> SendAsync(StorageRequest request)
        {
            CallContext<Mac>.SetData("qiniu_option", mac);
            var method = request.RequestMethod();
            var url = request.GetApiUrl();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = $"http://{url}";
            }

            var qiniuRequest = request as QiniuStorageRequest;
            var tokenType = qiniuRequest.GetTokenType();
            string contentType = qiniuRequest.ContentType();
            httpClient.LoadHandler(new QiniuHttpHandler(tokenType));
            if (method == "GET")
            {
                return await httpClient.GetAsync(url);
            }
            if (method == "POST")
            {
                string s = JsonSerializer.Serialize(request, request.GetType());
                HttpContent httpContent = new StringContent(s, Encoding.UTF8, contentType);
                if (s == "{}")
                {
                    httpContent = null;
                }

                if (contentType == "application/x-www-form-urlencoded"||contentType== "multipart/form-data")
                { 
                    httpContent = qiniuRequest.FormContent();
                }

                var res = await httpClient.PostAsync(url, httpContent);

                return res;

            }
            if (method == "DELETE")
            {

                var res = await httpClient.DeleteAsync(url);
                return (res, "");
            }
            return (null, null);

        }

        public async Task<T> SendAsync<T>(StorageRequest request)
         where T : StorageResponse
        {
            CallContext<Mac>.SetData("qiniu_option", mac);
            var method = request.RequestMethod();
            var url = request.GetApiUrl();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = $"http://{url}";
            }

            var qiniuRequest = request as QiniuStorageRequest;
            var tokenType = qiniuRequest.GetTokenType();

            httpClient.LoadHandler(new QiniuHttpHandler(tokenType));
            if (method == "GET")
            {
                return await httpClient.GetAsync<T>(url);
            }
            if (method == "POST")
            {
                string s = JsonSerializer.Serialize(request, request.GetType());
                string contentType = qiniuRequest.ContentType();

                HttpContent httpContent = new StringContent(s, Encoding.UTF8, contentType);
                if (s == "{}")
                {
                    httpContent = null;
                }

                if (contentType == "application/x-www-form-urlencoded")
                {

                    httpContent = qiniuRequest.FormContent();
                }

                var res = await httpClient.PostAsync<T>(url, httpContent);
                return res;

            }
            if (method == "DELETE")
            {
                var res = await httpClient.DeleteAsync<T>(url);
                return res;
            }
            return null;



        }
        public string GetUploadToken(string key,DateTime expiresTime)
        {
            //只允许上传指定key文件，key存在时不覆盖 insertOnly =1
            //只允许上传指定前缀文件，存在时不覆盖 isPrefixalScope =1

            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope =$"{storageOption.Bucket}:{key}";
            int exprieSeconds=(int)(expiresTime - DateTime.Now).TotalSeconds;
            putPolicy.SetExpires(exprieSeconds);
            putPolicy.isPrefixalScope = 1;
            putPolicy.InsertOnly = 1;
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            return token;
        }

        public async Task< TokenResult> GetUploadTokenInfoAsync(string key,DateTime expiresTime)
        {
            var token = GetUploadToken(key, expiresTime);
            var zone = await zoneHelper.QueryZoneAsync(mac, storageOption.Bucket);
            var upHost = zone.SrcUpHosts[0];
            TokenResult result = new TokenResult
            { 
                UploadUrl = $"https://{upHost}",
                Domain = storageOption.Domain,
                Token = token,
                Key=key
            };
            return result;
        }

        public string CreateDownloadUrl(string url, int expireInSeconds = 3600)
        {
            long deadline = UnixTimestamp.GetUnixTimestamp(expireInSeconds);
            string publicUrl = Uri.EscapeUriString(url);
            StringBuilder sb = new StringBuilder(publicUrl);
            if (publicUrl.Contains("?"))
            {
                sb.AppendFormat("&e={0}", deadline);
            }
            else
            {
                sb.AppendFormat("?e={0}", deadline);
            }

            string token = Auth.CreateDownloadToken(mac, sb.ToString());
            sb.AppendFormat("&token={0}", token);

            return sb.ToString();

        }




        #region 上传

        public async Task<UploadResult> UploadAsync(Stream stream,string key,string token)
        {
            await Task.Run(() =>
            {
                var config = new Qiniu.Storage.Config();
                Qiniu.Storage.UploadManager uploadManager = new Qiniu.Storage.UploadManager(config);
                Qiniu.Storage.PutExtra putExtra = new Qiniu.Storage.PutExtra();

                var ret = uploadManager.UploadStream(stream, key, token, putExtra);
                UploadResult result = new UploadResult();
                if (ret.Code == 200)
                {
                    result.Key = key;
                }
                return result;
            });

            return null;
        }

        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="filePath">本地文件路径</param>
        ///// <param name="key">新文件key</param>
        ///// <returns></returns>
        //public HttpResult UploadFile(string filePath,string key,bool IsUseHttps=false, string token=null)
        //{

        //    Qiniu.Storage.Config config = new Qiniu.Storage.Config();
        //    config.Zone = zone; 
        //    config.UseHttps = IsUseHttps;
        //    config.UseCdnDomains = true;
        //    config.ChunkSize = ChunkUnit.U512K;
        //    FormUploader target = new FormUploader(config);

        //    if(string.IsNullOrEmpty(token))
        //    {
        //        token = GetUploadToken().Token;
        //    }
        //    var result = target.UploadFile(filePath, key, token, null);

        //    return result;
        //}

        //public HttpResult Upload(byte[] fileData, string saveKey, string token)
        //{
        //    Qiniu.Storage.Config config = new Qiniu.Storage.Config();
        //    config.Zone = zone;

        //    FormUploader formUploader = new FormUploader(config);
        //    PutExtra putExtra = new PutExtra();
        //    return formUploader.UploadData(fileData, saveKey, token, putExtra);
        //}

        //public HttpResult Upload(string localFile, string saveKey, string token)
        //{
        //    PutExtra putExtra = new PutExtra();
        //    Qiniu.Storage.Config config = new Qiniu.Storage.Config();
        //    config.Zone = zone;
        //    //表单提交
        //    UploadManager um = new UploadManager(config);
        //    var result = um.UploadFile(localFile, saveKey, token, putExtra);

        //    return result;
        //}
        #endregion




        public async Task<UploadResult> UploadAsync()
        {
            //await Task.Run(() =>
            //{
            //    UploadResult result = new UploadResult();
            //    var request = httpContext.HttpContext.Request.Form;
            //    string key = httpContext.HttpContext.Request.Form["key"];
            //    var token = httpContext.HttpContext.Request.Form["token"];
            //    var files = httpContext.HttpContext.Request.Form.Files;

            //    long size = files.Sum(f => f.Length); //统计所有文件的大小

            //    UploadManager uploadManager = new UploadManager(config);
            //    PutExtra putExtra = new PutExtra();

            //    var ret=uploadManager.UploadStream(files.FirstOrDefault().OpenReadStream(), key, token, putExtra);
            //    if(ret.Code==200)
            //    {
            //        result.Key = key; 
            //    }
            //    return result;
            //});
            return null;
        }

     

      
        
    }  
}
