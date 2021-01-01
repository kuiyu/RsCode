using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    public class UploadAbortRequest:StorageRequest
    {
        public UploadAbortRequest(string bucket,string objectName,string uploadId)
        {
            BucketName=bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
        }
        string BucketName;
        string EncodedObjectName;
        string UploadId;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/buckets/{BucketName}/objects/{EncodedObjectName}/uploads/{UploadId}";
        }
    }
}
