
using Microsoft.Extensions.Options;
using RsCode.Storage.QiniuStorage;
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.Storage.Tests
{
    public class ServiceTest
    {
        IStorageProvider qiniu;
        //QiniuHttpClient httpClient;
        HttpClient httpClient;
        QiniuOptions options;
        public ServiceTest(IEnumerable<IStorageProvider> storageProvider
            //,QiniuHttpClient _httpClient
            ,HttpClient _httpClient,
             IOptionsSnapshot<QiniuOptions> _options
            )
        {
            qiniu = storageProvider.FirstOrDefault(s=>s.StorageName=="qiniu");
            httpClient = _httpClient;
            options = _options.Value;
        }
        [Fact]
        public void MangerToken()
        {
            string ak = "MY_ACCESS_KEY";
            string sk = "MY_SECRET_KEY";
            string url = "http://rs.qiniu.com/move/bmV3ZG9jczpmaW5kX21hbi50eHQ=/bmV3ZG9jczpmaW5kLm1hbi50eHQ=";
            string method = "POST";
            var signTool = new Signature(new Mac(ak, sk));
            string accessToken = signTool.Sign(url, method);

            Assert.Equal("MY_ACCESS_KEY:1uLvuZM6l6oCzZFqkJ6oI4oFMVQ=", accessToken);
        }

        [Fact]
        public async Task BucketList()
        {
            // Qiniu.Storage.Config config = new Qiniu.Storage.Config(); 
            // Qiniu.Storage.BucketManager bucketManager = new Qiniu.Storage.BucketManager(new Qiniu.Util.Mac(options.AccessKey, options.SecretKey), config);
            //var  buckets= bucketManager.Buckets(true);          

            var ret= await qiniu.SendAsync(new BucketQueryRequest());
            if(ret.StatusCode== System.Net.HttpStatusCode.OK)
            {
                var buckets = JsonSerializer.Deserialize<string[]>(await ret.Content.ReadAsStringAsync());
                
                Assert.NotNull(buckets);
            }

        }



    }
}
