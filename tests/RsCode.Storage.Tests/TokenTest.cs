using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RsCode.Storage.Tests
{
    public class TokenTest
    {
        IStorageProvider qiniu;
        public TokenTest(IEnumerable<IStorageProvider> storageProviders)
        {
            qiniu = storageProviders.FirstOrDefault(c => c.StorageName == "qiniu");
        }

        [Fact]
        public void UploadTokenTest()
        {
            string bucket = "rscode";
            var tokenInfo = qiniu.GetUploadToken($"{bucket}:abc", DateTime.Now.AddDays(1));
            Assert.NotNull(tokenInfo);
        }
    }
}
