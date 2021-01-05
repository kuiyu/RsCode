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
    public class CopyRequest: QiniuStorageRequest
    {
        public CopyRequest(string bucket,string entryUriSrc, string entryURIDest, bool force)
        {
            Bucket = bucket;
            EncodedEntryURISrc = Core.Base64.UrlSafeBase64Encode(entryUriSrc);
            EncodedEntryURIDest = Core.Base64.UrlSafeBase64Encode(entryURIDest);
            Force = force;
        }
        string Bucket;
        bool Force;
        string EncodedEntryURISrc;
        string EncodedEntryURIDest;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/copy/{EncodedEntryURISrc}/{EncodedEntryURIDest}/force/{Force.ToString()}";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
