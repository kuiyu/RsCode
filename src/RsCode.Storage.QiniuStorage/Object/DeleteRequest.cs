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
    /// 删除指定资源。如果资源不存在，则返回错误码612。
    /// <see cref="https://developer.qiniu.com/kodo/1257/delete"/>
    /// </summary>
    public class DeleteRequest: QiniuStorageRequest
    {
        string Bucket;
        string EncodedEntryURI;
        public DeleteRequest(string bucket,string key)
        {
            Bucket = bucket;
            EncodedEntryURI = Base64.UrlSafeBase64Encode(bucket,key) ;
        }
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/delete/{EncodedEntryURI}";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
