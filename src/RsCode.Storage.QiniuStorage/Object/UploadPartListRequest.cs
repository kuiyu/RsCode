using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage.Object
{
    /// {summary}
    /// 列举已上传分片
    /// {see cref="https://developer.qiniu.com/kodo/6858/listparts"/}
    /// {/summary}
    public class UploadPartListRequest:StorageRequest
    {
        public UploadPartListRequest(string bucket, string objectName, string uploadId,int maxParts,int partNumberMarker)
        {
            BucketName = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
            MaxParts = maxParts;
            PartNumberMarker = partNumberMarker;
        }
        string BucketName;
        string EncodedObjectName;
        string UploadId;
        int MaxParts;
         int PartNumberMarker;

        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/buckets/{BucketName}/objects/{EncodedObjectName}/uploads/{UploadId}?max-parts={MaxParts}&part-number-marker={PartNumberMarker}";
        }
    }
}
