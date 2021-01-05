using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 分块上传数据
    /// <see cref="https://developer.qiniu.com/kodo/6366/upload-part"/>
    /// </summary>
    public class UploadPartRequest: QiniuStorageRequest
    {
        public UploadPartRequest(string bucket,string objectName,string uploadId,int partNumber)
        {
            Bucket = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
            PartNumber = partNumber;
        }
        string Bucket;
        string EncodedObjectName;
        string UploadId;
        int PartNumber;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ServerUploadDomain;
            return $"{url}/buckets/{Bucket}/objects/{EncodedObjectName}/uploads/{UploadId}/{PartNumber}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Upload;
        }
    }
}
