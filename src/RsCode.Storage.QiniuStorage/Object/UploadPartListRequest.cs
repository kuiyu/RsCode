using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{

/// <summary>
/// 列举已上传分片
/// <see cref="https://developer.qiniu.com/kodo/6858/listparts"/>
/// </summary>
public class UploadPartListRequest: QiniuStorageRequest
{
        public UploadPartListRequest(string bucket, string objectName, string uploadId,int maxParts,int partNumberMarker)
        {
            Bucket = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
            MaxParts = maxParts;
            PartNumberMarker = partNumberMarker;
        }
        string Bucket;
        string EncodedObjectName;
        string UploadId;
        int MaxParts;
         int PartNumberMarker;

        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ServerUploadDomain;
            return $"{url}/buckets/{Bucket}/objects/{EncodedObjectName}/uploads/{UploadId}?max-parts={MaxParts}&part-number-marker={PartNumberMarker}";
        }
    }
}
