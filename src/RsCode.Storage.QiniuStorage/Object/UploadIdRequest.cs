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
            var url = zone.SrcUpHosts[0];
            return $"{url}/buckets/{Bucket}/objects/{EncodedObjectName}/uploads";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Upload;
        }
    }
}
