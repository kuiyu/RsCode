using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public class UploadCompleteRequest:StorageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="objectName">资源名</param>
        /// <param name="uploadId">在服务端申请的 MultipartUpload 任务 id</param>
        public UploadCompleteRequest(string bucket,string objectName,string uploadId)
        {
            BucketName = bucket;
            EncodedObjectName = Core.Base64.UrlSafeBase64Encode(objectName);
            UploadId = uploadId;
        }
        string BucketName;
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
            return $"{Config.DefaultApiHost}/buckets/{BucketName}/objects/{EncodedObjectName}/uploads/{UploadId}";
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
