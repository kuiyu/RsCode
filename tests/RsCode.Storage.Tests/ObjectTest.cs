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
            string bucket="rsyunpan";
            var ret = await qiniu.SendAsync<ListResponse>(new ListRequest(bucket,"",1000, $"image"));
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
    }
}
