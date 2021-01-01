using RsCode.Storage.QiniuStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
