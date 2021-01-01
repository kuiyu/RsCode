using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 分块上传数据
    /// </summary>
    public class UploadPartRequest:StorageRequest
    {
        public UploadPartRequest(string bucket,string objectName,string uploadId,int partNumber)
        {
            BucketName = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
            PartNumber = partNumber;
        }
        string BucketName;
        string EncodedObjectName;
        string UploadId;
        int PartNumber;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/buckets/{BucketName}/objects/{EncodedObjectName}/uploads/{UploadId}/{PartNumber}";
        }
    }
}
