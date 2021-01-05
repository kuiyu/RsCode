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
