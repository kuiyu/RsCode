/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using RsCode.Storage.QiniuStorage.Core;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 资源元信息查询
    /// <see cref="https://developer.qiniu.com/kodo/1308/stat"/>
    /// </summary>
    public class StatRequest:QiniuStorageRequest
    {
        public StatRequest(string bucket,string key)
        {
            Bucket = bucket;
            EncodedEntryURI = Core.Base64.UrlSafeBase64Encode(bucket,key);
        }
        string EncodedEntryURI;
        string Bucket;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/stat/{EncodedEntryURI}";
        }
        public override string RequestMethod()
        {
            return "GET";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }

       
    }

    
}
