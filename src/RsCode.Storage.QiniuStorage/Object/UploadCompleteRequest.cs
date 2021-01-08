/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 完成文件上传
    /// <see cref="https://developer.qiniu.com/kodo/6368/complete-multipart-upload"/>
    /// </summary>
    public class UploadCompleteRequest: QiniuStorageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="objectName">资源名</param>
        /// <param name="uploadId">在服务端申请的 MultipartUpload 任务 id</param>
        public UploadCompleteRequest(string bucket,string objectName,string uploadId)
        {
            Bucket = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
        }
        string Bucket;
        string EncodedObjectName;
        string UploadId;

        [JsonPropertyName("parts")]
        public PartInfo[]  Parts { get; set; }

        [JsonPropertyName("fname")]
        public string FileName { get; set; }

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string,string> Metadata { get; set; }

        [JsonPropertyName("customVars")]
        public Dictionary<string,string> CustomVars { get; set; }

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

    public class PartInfo
    {
        [JsonPropertyName("partNumber")]
        public int PartNumber { get; set; }
        [JsonPropertyName("etag")]
        public string Etag { get; set; }
    }
}
