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
