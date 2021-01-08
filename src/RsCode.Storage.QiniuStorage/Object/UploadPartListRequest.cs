/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;

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
