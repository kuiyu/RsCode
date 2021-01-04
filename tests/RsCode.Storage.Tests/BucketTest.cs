using RsCode.Storage.QiniuStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.Storage.Tests
{
    public class BucketTest
    {
        string tmpBucket = "rstestest";
        IStorageProvider qiniu;
        
        public BucketTest(IEnumerable<IStorageProvider> storageProviders)
        {
            qiniu = storageProviders.FirstOrDefault(c => c.StorageName=="qiniu");
           
        }

        [Fact]
        public async Task BucketCreate()
        {
            
           var ret= await qiniu.SendAsync(new BucketCreateRequest(tmpBucket));
            Assert.Equal(200, (int)ret.StatusCode);
        }

        [Fact]
        public async Task BucketRemove()
        {
            var ret = await qiniu.SendAsync(new BucketRemoveRequest(tmpBucket));
            Assert.Equal(200, (int)ret.StatusCode);
        }
        //获取 Bucket 空间域名
        [Fact]
        public async Task BucketDomain()
        {
            var ret = await qiniu.SendAsync(new BucketDomainRequest("res-rscode-cn"));
            if((int)ret.StatusCode==200)
            {
                string[] res = JsonSerializer.Deserialize<string[]>(await ret.Content.ReadAsStringAsync());
                Assert.NotNull(res);
            } 
        }

        [Fact]
        public async Task SetPrivateTest()
        { 
            var ret = await qiniu.SendAsync(new BucketAuthRequest("www-hnrswl-com", 1));
            Assert.Equal(200, (int)ret.StatusCode);
        }
    }
}
