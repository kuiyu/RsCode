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
    /// <summary>
    /// 资源元信息修改
    /// <see cref="https://developer.qiniu.com/kodo/1252/chgm"/>
    /// </summary>
    public class ChgmRequest: QiniuStorageRequest
    {
        string EncodedEntryURI;
        string EncodedMimeType;
        string meta_key;
        string EncodedMetaValue;
        string Encodedcond;
        string Bucket;
        public ChgmRequest(string bucket,string key,string mimeType,string metaKey,string metaValue,string cond)
        {
            Bucket = bucket;

            EncodedEntryURI = Core.Base64.UrlSafeBase64Encode(key);
            EncodedMimeType = Core.Base64.UrlSafeBase64Encode(mimeType);
            meta_key = metaKey;
            EncodedMetaValue =Core.Base64.UrlSafeBase64Encode(metaValue);
            Encodedcond = Core.Base64.UrlSafeBase64Encode(cond);
        }
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/chgm/{EncodedEntryURI}/mime/{EncodedMimeType}/x-qn-meta-{meta_key}/{EncodedMetaValue}/cond/{Encodedcond}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
