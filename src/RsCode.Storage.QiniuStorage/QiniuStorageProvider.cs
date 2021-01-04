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
using Qiniu.Http;


using RsCode.Storage.QiniuStorage;
using RsCode.Storage.QiniuStorage.Core;
using RsCode.Threading;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage
{
    public class QiniuStorageProvider : IStorageProvider
    {
        IHttpContextAccessor httpContext;
        QiniuOptions options;
        Mac mac;
      
        string uploadUrl = "";
        QiniuHttpClient httpClient;

        public QiniuStorageProvider(
            IOptionsSnapshot<QiniuOptions> _options,
            IHttpContextAccessor _httpContext,
            QiniuHttpClient _qiniuHttpClient
           
            )
        {
            httpClient = _qiniuHttpClient;
            options = _options.Value;
            mac = new Mac(options.AccessKey, options.SecretKey);
           
            httpContext = _httpContext; 
            
        }
        public string StorageName { get; } = "qiniu";


      


        #region 鉴权
        public TokenResult GetUploadToken(bool isClient = true)
        {
            return null;
            //int uploadType = isClient?1:0;
            //int OutTime = options.UploadTokenExpireTime; //超时时间 单位秒
             

            //PutPolicy putPolicy = new PutPolicy();
            //putPolicy.Scope = options.Bucket;
            //putPolicy.SetExpires(OutTime);

            //string jstr = putPolicy.ToJsonString();
            //string token = Auth.CreateUploadToken(mac, jstr);


            //TokenResult result = new TokenResult
            //{
            //    UploadUrl = $"https://{config.Zone.SrcUpHosts[uploadType]}",
            //    Domain = options.Domain,
            //    Token = token
            //};
            //return result;
        }
        public TokenResult GetDownloadToken(bool isClient = true)
        {
            return null;
            //int OutTime = options.DownloadTokenExpireTime; //超时时间 单位秒
            
            //PutPolicy putPolicy = new PutPolicy();
            //putPolicy.Scope = options.Bucket;
            //putPolicy.SetExpires(OutTime);

            //string url = "";
            //string jstr = putPolicy.ToJsonString();
            //string token = Auth.CreateDownloadToken(mac, url);

            //TokenResult result = new TokenResult
            //{
            //    Domain = options.Domain,
            //    Token = token
            //};
            //return result;
        }

        public TokenResult GetManageToken(bool isClient = true)
        {
            return null;
            //int OutTime = options.ManageTokenExpireTime; //超时时间 单位秒
            

            //PutPolicy putPolicy = new PutPolicy();
            //putPolicy.Scope = options.Bucket;
            //putPolicy.SetExpires(OutTime);

            //string jstr = putPolicy.ToJsonString();
            //string token = Auth.CreateManageToken(mac, jstr);
            //TokenResult result = new TokenResult
            //{
            //    Domain = options.Domain,
            //    Token = token
            //};
            //return result;
        }
        

       
        #endregion

        #region 上传

        

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
        ///// <summary>
        ///// 公开空间的文件下载
        ///// </summary>
        ///// <param name="key">云端文件key</param>
        ///// <returns>返回文件下载地址</returns>
        //public string CreatePublicshUrl(string key)
        //{
        //    string domain =options. Domain;
        //    string publicUrl = DownloadManager.CreatePublishUrl(domain, key);
        //    return publicUrl;
        //}

        //public string CreatePrivateUrl(string key,int ExpireInSeconds=3600)
        //{
            
        //    string domain = options.Domain;
        //    string privateUrl = DownloadManager.CreatePrivateUrl(mac, domain, key, ExpireInSeconds);
        //    return privateUrl;
        //}
        


        ///// <summary>
        ///// 查询某key是否存在
        ///// </summary>
        ///// <param name="bucket"></param>
        ///// <param name="key"></param>
        ///// <returns>如果存在返回Code=200,不存在返回Code=612</returns>
        //public StatResult Query(string bucket, string key)
        //{ 
        //    BucketManager bm = new BucketManager(mac,config);
        //    StatResult result = bm.Stat(bucket, key);
        //    return result;
        //}

        ///// <summary>
        ///// 批量查询
        ///// </summary>
        ///// <param name="batchOps">batch批处理请求的主体内容具有op=OP1&op=OP2&op=...这样的格式，其中op=OPS作为一个单位，用&符号相连</param>
        //public BatchResult BatchQuery(List<string> batchOps)
        //{ 
        //    BucketManager bm = new BucketManager(mac,config);
        //    var result = bm.Batch(batchOps);
        //    return result;
        //}

        ///// <summary>
        ///// 删除指定的key
        ///// </summary>
        ///// <param name="bucket"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public HttpResult Delete(string bucket, string key)
        //{  
        //    BucketManager bm = new BucketManager(mac,config);
        //    var result = bm.Delete(bucket, key);

        //    if(result.Code==200)
        //    {
        //        RefreshUrls(new string[] { key });
        //    }
        //    return result;
        //}

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="oldKey">原文件路径</param>
        /// <param name="newKey">新文件路径</param>
        /// <returns></returns>
        public HttpResult Move(string bucket, string oldKey, string newKey)
        {
            //BucketManager bm = new BucketManager(mac,config);

            //// 是否设置强制覆盖
            //Boolean force = true;
            //HttpResult result = bm.Move(bucket, oldKey, bucket, newKey, force);
            //if(result.Code==200)
            //{
            //    RefreshUrls(new string[] { oldKey });
            //}
            //return result;
            return null;
        }


        //public HttpResult Rename(string bucket, string oldkey, string newKey)
        //{
        //    Qiniu.Storage.Config config = new Qiniu.Storage.Config();
        //    
        //    BucketManager bm = new BucketManager(mac,config);
        //    var result = bm.Rename(bucket, oldkey, newKey);
             
        //    return result;
        //}

        ///// <summary>
        ///// 缓存刷新-刷新URL
        ///// </summary>
        ///// <param name="urls">要刷新的URL列表</param>
        ///// <returns>缓存刷新的结果</returns>
        //public HttpResult RefreshUrls(string[] urls)
        //{
        //    CdnManager cdnManager = new CdnManager(mac);
        //    return cdnManager.RefreshUrls(urls);
        //}

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

        public async Task<T>SendAsync<T>(StorageRequest request)
            where T:StorageResponse
        {
           var response= await SendAsync(request);
            if(response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SendAsync(StorageRequest request) 
        {
            CallContext<Mac>.SetData("qiniu_option",mac);
            var method = request.RequestMethod();
            var url = request.GetApiUrl();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = $"http://{url}";
            }

            var qiniuRequest = request as QiniuStorageRequest;
            var tokenType= qiniuRequest.GetTokenType(); 
              
            httpClient.LoadHandler(new QiniuHttpHandler(tokenType));
            if (method == "GET")
            {
                return await httpClient.GetAsync(url);
            }
            if (method == "POST")
            {
                string s = JsonSerializer.Serialize(request, request.GetType());
                HttpContent httpContent = new StringContent(s, Encoding.UTF8, "application/json");
                if(s=="{}")
                {
                    httpContent = null;
                }
                 
                //HttpContent httpContent = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
               
                var res = await httpClient.PostAsync(url, httpContent);
                return res;

            }
            if (method == "DELETE")
            {
              
                var res = await httpClient.DeleteAsync(url);
                return res;
            }
            return null;

        }
    }  
}
