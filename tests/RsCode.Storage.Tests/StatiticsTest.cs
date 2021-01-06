using RsCode.Storage.QiniuStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.Storage.Tests
{
   public class StatiticsTest
    {
        IStorageProvider qiniu;
        public StatiticsTest(IStorageProvider _qiniu)
        {
            qiniu = _qiniu;
        }


        [Fact]
        public async Task SpaceTest()
        {
            string bucket = "ttj-test";
            DateTime beginTime = Convert.ToDateTime("2020-7-1");
            DateTime endTime = Convert.ToDateTime("2021-1-1");
            var ret = await qiniu.SendAsync<SpaceResponse>(new SpaceRequest(bucket, beginTime, endTime, QiniuStorage.Core.Region.z0));
            Assert.Equal(200, ret.HttpCode);
        }


        [Fact]
        public async Task CountTest()
        {
            string bucket = "ttj-test";
            DateTime beginTime = Convert.ToDateTime("2020-1-1");
            DateTime endTime = Convert.ToDateTime("2021-1-1");
            var ret = await qiniu.SendAsync<CountResponse>(new CountRequest(bucket, beginTime, endTime, QiniuStorage.Core.Region.z0));
            Assert.Equal(200, ret.HttpCode);
        }

      
    }
}
