/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    public class ChgmRequest:StorageRequest
    {
        string EncodedEntryURI;
        string EncodedMimeType;
        string meta_key;
        string EncodedMetaValue;
        string Encodedcond;
        public ChgmRequest(string encodedEntryUri,string encodeMimeType,string metaKey,string encodedMetaValue,string encodedcond)
        {
            EncodedEntryURI = encodedEntryUri;
            EncodedMimeType = encodeMimeType;
            meta_key = metaKey;
            EncodedMetaValue = encodedMetaValue;
            Encodedcond = encodedcond;
        }
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/chgm/{EncodedEntryURI}/mime/{EncodedMimeType}/x-qn-meta-{meta_key}/{EncodedMetaValue}/cond/{Encodedcond}";
        }
    }
}
