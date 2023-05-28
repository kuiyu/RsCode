namespace RsCode.Douyin.Tests
{
    public class AccessTokenTest
    {
        IDouyinClient douyinClient;
        public AccessTokenTest(IDouyinClient douyinClient)
        {
            this.douyinClient = douyinClient;
        }
        [Fact]
        public async Task GetAccessTokenTest()
        {
            var options = douyinClient.UseAppId("tt6be31d5ef4dfda0b01");
            var ret = await douyinClient.SendAsync<AccessTokenResponse>(new AccessTokenRequest { AppId = options.AppId, AppSecret = options.Secret });
            Assert.Equal(0, ret.ErrNo);
        }
    }
}