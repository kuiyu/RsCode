using RsCode.Storage.QiniuStorage;
using System.Collections.Generic;
using System.Linq;
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
            
           var (ret,s)= await qiniu.SendAsync(new BucketCreateRequest(tmpBucket));
            Assert.Equal(200, (int)ret.StatusCode);
        }

        [Fact]
        public async Task BucketRemove()
        {
            var (ret, s) = await qiniu.SendAsync(new BucketRemoveRequest(tmpBucket));
            Assert.Equal(200, (int)ret.StatusCode);
        }
        //获取 Bucket 空间域名
        [Fact]
        public async Task BucketDomain()
        {
            var (ret, s) = await qiniu.SendAsync(new BucketDomainRequest("res-rscode-cn"));
            if((int)ret.StatusCode==200)
            {
                string[] res = JsonSerializer.Deserialize<string[]>(await ret.Content.ReadAsStringAsync());
                Assert.NotNull(res);
            } 
        }
     
       
        //设置bucket访问权限
        [Fact]
        public async Task SetPrivateTest()
        { 
            var ret = await qiniu.SendAsync<BucketAuthResponse>(new BucketAuthRequest("rsyunpan", 1));
            Assert.Equal(200, ret.HttpCode);
           // Assert.Equal(200, (int)ret.StatusCode);
        }

        //查看空间标签
        [Fact]
        public async Task BucketTagging()
        {
            var ret = await qiniu.SendAsync<BucketTaggingResponse>(new BucketTaggingRequest("ttj-test"));
            //  Assert.Equal(200, (int)ret.StatusCode);
            Assert.NotNull(ret);
        }
    }
}
