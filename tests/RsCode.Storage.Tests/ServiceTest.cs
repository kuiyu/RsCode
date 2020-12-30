using Qiniu.Util;
using RsCode.Storage.QiniuStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RsCode.Storage.Tests
{
    public class ServiceTest
    {
        IStorageProvider qiniu;
        public ServiceTest(IEnumerable<IStorageProvider> storageProvider)
        {
            qiniu = storageProvider.FirstOrDefault(s=>s.StorageName=="qiniu");
        }
        [Fact]
        public void GetBucket()
        {
            string ak = "";
            string sk = "";
            Mac mac = new Mac(ak, sk);
           string token= Auth.CreateManageToken(mac, "http://rs.qbox.me/buckets");

           var ret= qiniu.SendAsync<BucketQueryResponse>(new BucketQueryRequest());
        }
    }
}
