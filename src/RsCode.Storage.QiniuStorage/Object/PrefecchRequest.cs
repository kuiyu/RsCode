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
    public class PrefecchRequest:QiniuStorageRequest
    {
        string EncodedEntryURI;
        string Bucket;
        public PrefecchRequest(string bucket,string url)
        {
            Bucket = bucket;
            EncodedEntryURI =Core.Base64.UrlSafeBase64Encode(bucket,url);
        }
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.IovipHost;
            return $"{url}/prefetch/{EncodedEntryURI}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
