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
            var url = "1197/wxSop/59420fc80a95a1f70f289afa9aea21c0.jpg";
            var ret = await qiniu.SendAsync<ChStatusResponse>(new ChStatusRequest(url, 0));
            Assert.NotNull(ret);
        }
    }
}
