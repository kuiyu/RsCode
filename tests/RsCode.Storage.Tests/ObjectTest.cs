using RsCode.Storage.QiniuStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.Storage.Tests
{

    public class ObjectTest
    {
        IStorageProvider qiniu;
        public ObjectTest(IEnumerable<IStorageProvider> storageProviders)
        {
            qiniu = storageProviders.FirstOrDefault(c => c.StorageName == "qiniu");
        }

        [Fact]
        public async Task ListTest()
        {
            string bucket="ttj-test";
            var ret = await qiniu.SendAsync<ListResponse>(new ListRequest(bucket,"",1000, $"1698/","/"));
            Assert.Equal(200, ret.HttpCode);
        }

        [Fact]
        public async Task DownFileTest()
        {
            string key = "1698/wxSop/0300602655adcfc96799c0e1d5e9d907.jpg";

            var ret =  qiniu.CreateDownloadUrl("http://img0.tutaojin.net",key,10);
            //cdn缓存时间优先于指定的有效时间
            Assert.NotNull(ret);
        }

        //修改文件状态
        [Fact]
        public async Task ChangeStatus()
        {
            var url = "/note/project/logo/0rk1qechokcr.png";
            var ret = await qiniu.SendAsync<ChStatusResponse>(new ChStatusRequest("res-rscode-cn", url,1));

            Assert.Equal(200,ret.HttpCode);
        }

        [Fact]
        public async Task Stat()
        {
            string bucket = "ttj-test";
            string key = "1698/wxSop/0efdd7d228ffe03d00530c05a73462f8.jpg";
            var ret = await qiniu.SendAsync<StatResponse>(new StatRequest(bucket, key));
        }

        [Fact]
        public async Task Batch()
        {
            string key1 = "1197/wxSop/59420fc80a95a1f70f289afa9aea21c0.jpg";
            string key2 = "1698/wxSop/0300602655adcfc96799c0e1d5e9d907.jpg";
            string bucket = "ttj-test";
            List<QiniuStorageRequest> ops = new List<QiniuStorageRequest>(); 
 
            var url = new StatRequest(bucket, key2).GetApiUrl();
            var s = url.Substring(url.IndexOf('/'));
            var url2 = new StatRequest(bucket, key1).GetApiUrl();
            var s2 = url2.Substring(url2.IndexOf('/'));

            ops.Add(new StatRequest(bucket, key1));
            ops.Add(new StatRequest(bucket, key2)); 

            var (ret,result) = await qiniu.SendAsync(new BatchRequest(bucket,ops)); 
            
        }
    }
}
