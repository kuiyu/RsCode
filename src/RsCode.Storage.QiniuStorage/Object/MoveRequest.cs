/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    public class MoveRequest: QiniuStorageRequest
    {
        public MoveRequest(string bucket,string entryUriSrc,string entryURIDest,bool force)
        {
            Bucket = bucket;
            EncodedEntryURISrc = Core.Base64.UrlSafeBase64Encode(entryUriSrc);
            EncodedEntryURIDest = Core.Base64.UrlSafeBase64Encode(EncodedEntryURIDest);  
            Force=force;
        }
        string Bucket;
        bool Force;
        string EncodedEntryURISrc;
        string EncodedEntryURIDest;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/move/{EncodedEntryURISrc}/{EncodedEntryURIDest}/force/{Force.ToString()}";
        }
    }
}
