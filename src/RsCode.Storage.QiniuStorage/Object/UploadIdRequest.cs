using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 获取一个全局唯一的 UploadId
    /// <see cref="https://developer.qiniu.com/kodo/6365/initialize-multipartupload"/>
    /// </summary>
    public class UploadIdRequest: QiniuStorageRequest
    {
        public UploadIdRequest(string bucket,string objectName="`")
        {
            EncodedObjectName =objectName=="`"?objectName: Core.Base64.UrlSafeBase64Encode(objectName);
            Bucket = bucket;
        }
        string Bucket;
        string EncodedObjectName;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ServerUploadDomain;
            return $"{url}/buckets/{Bucket}/objects/{EncodedObjectName}/uploads";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Upload;
        }
    }
}
