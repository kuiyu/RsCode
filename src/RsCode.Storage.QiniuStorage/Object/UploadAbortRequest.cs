
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 终止上传
    /// <see cref="https://developer.qiniu.com/kodo/6367/abort-multipart-upload"/>
    /// </summary>
    public class UploadAbortRequest: QiniuStorageRequest
    {
        public UploadAbortRequest(string bucket,string objectName,string uploadId)
        {
            Bucket=bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
        }
        string Bucket;
        string EncodedObjectName;
        string UploadId;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ServerUploadDomain;
            return $"{url}/buckets/{Bucket}/objects/{EncodedObjectName}/uploads/{UploadId}";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Upload;
        }
    }
}
