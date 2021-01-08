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
