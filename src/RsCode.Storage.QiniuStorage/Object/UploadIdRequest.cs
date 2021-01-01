using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 获取一个全局唯一的 UploadId
    /// <see cref="https://developer.qiniu.com/kodo/6365/initialize-multipartupload"/>
    /// </summary>
    public class UploadIdRequest:StorageRequest
    {
        public UploadIdRequest(string bucket,string objectName="`")
        {
            EncodedObjectName =objectName=="`"?objectName: Core.Base64.UrlSafeBase64Encode(objectName);
            BucketName = bucket;
        }
        string BucketName;
        string EncodedObjectName;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/bucket/{BucketName}/objects/{EncodedObjectName}/uploads";
        }
    }
}
