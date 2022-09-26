using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using static AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleResponseBody;

namespace RsCode.Storage.Aliyun
{
    public  interface IAliyunOssService
    {
        Task<AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleResponseBody> GetStsTokenAsync( string sessionName,string endpointName, string roleArn = "", int durationSeconds = 900);

        AlibabaCloud.SDK.Sts20150401.Client CreateStsClient(string accessKeyId, string accessKeySecret, string endpoint);

        Task<PutObjectResult> UploadFileAsync(IFormFile file, string key, string bucketName, string endpointName, AssumeRoleResponseBodyCredentials stsToken);

        Task<PutObjectResult> UploadFileAsync(Stream fs, string key, string bucketName, string endpointName, AssumeRoleResponseBodyCredentials stsToken);
        /// <summary>
        /// 获取临时访问的url
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="endpointName"></param>
        /// <param name="stsToken"></param>
        /// <returns></returns>
        Uri GeneratePresignedUri(string bucketName, string key, string endpointName, AssumeRoleResponseBodyCredentials stsToken);
        Task<DeleteObjectResult> DeleteFileAsync(string bucketName, string key, string endpointName, AssumeRoleResponseBodyCredentials stsToken);
        /// <summary>
        /// 查询指定key对应UrL
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sessionName"></param>
        /// <param name="bucketName"></param>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        Task<string> GetImgUrl(string key, string sessionName, string bucketName = "rswl-resource", string endpointName = "cn-shanghai");
    }
}
