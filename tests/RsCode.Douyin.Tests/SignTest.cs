using RsCode.Douyin.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Douyin.Tests
{
    public class SignTest
    {
        IDouyinClient douyinClient;
        public SignTest(IDouyinClient douyinClient)
        {
            this.douyinClient = douyinClient;
        }
        [Fact]
        public void CheckSignTest()
        {
            string s = "POST\n/abc\n1680835692\ngjjRNfQlzoDIJtVDOfUe\n{\"eventTime\":1677653869000,\"status\":102}\n";
            string exp = "RFQ65hHlo4xyZ6EC31LZC0SzsyN0nd2Fb2wAiISvY1mkiC6G8gn2QZwLGq7qgjenRGl/Z8OrTtkBHWb9GOazkJFkHrPeRqogqnwZ+kSOxGvtou8FPN669E1wwb+BShN4pIUgPFzaukR9/rCRBsbLoq9RPVA2sbf3iKoHGa81zhXjQSuFbF1CyiWkL5qqniNTM/BSfwfLZfPW8nBanRl3U+mQaymbj0DCF0ZdWhFz1FnZPAfEpx8YEwFNZWtxzz4p3WJ1swnUocJC4LXoDazo6DhEPDuoZXOXrB1SqzL1wRqA4p8uj3Z8Seki/PMGWiGpGWPMv3tJyvWmzMOuVJtEjg==";
            var options = douyinClient.UseAppId("tt6be31d5ef4dfda0b01");
            string ret = DouyinTool.Sign(s, options);
            Assert.Equal(exp, ret);
        }

        [Fact]
        public void ParamSign()
        {
            var options = douyinClient.UseAppId("tt6be31d5ef4dfda0b01");
            PrepayRequest request = new PrepayRequest(options.AppId, "abdeiekd", 18, "http://rscode.cn");
            var json = JsonSerializer.Serialize(request);
            var sign=request.CreateSign(options);
            Assert.Equal("61a3191ee426dc64f6f409052a0653b7", sign);
        }
    }
}
