using Aliyun.OSS;
using System;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using static AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleResponseBody;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RsCode.Storage.Aliyun
{
    public class AliyunOssService : IAliyunOssService
    {

        string AccessKeyId { get; set; }
        string AccessKeySecret { get; set; }

        AliyunStorageOptions Options;



        public AliyunOssService(IOptionsSnapshot<AliyunStorageOptions> optionsSnapshot)
        {
            Options = optionsSnapshot.Value;
            if (Options == null)
                throw new Exception("aliyun oss config err");

            AccessKeyId = optionsSnapshot.Value.AccessKeyId;
            AccessKeySecret = optionsSnapshot.Value.AccessKeySecret;
        }

        public async Task<AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleResponseBody> GetStsTokenAsync(string sessionName, string endpointName, string roleArn = "", int durationSeconds = 3600)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionName))
                {
                    throw new Exception("sessionName不能为空");
                }
                AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleResponseBody body = null;
                await Task.Run(() =>
                {
                    var endpoint = Options.GetEndPoint(endpointName);
                    var client = CreateStsClient(AccessKeyId, AccessKeySecret, endpoint.Sts);
                    if (string.IsNullOrEmpty(roleArn))
                    {
                        roleArn = Options.RoleArn[0];
                    }
                    AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleRequest assumeRoleRequest = new AlibabaCloud.SDK.Sts20150401.Models.AssumeRoleRequest();
                    assumeRoleRequest.DurationSeconds = durationSeconds;
                    assumeRoleRequest.RoleArn = roleArn;
                    assumeRoleRequest.RoleSessionName = sessionName; //自定义会话名称

                    var response = client.AssumeRole(assumeRoleRequest);
                    body = response.Body;
                });
                return body;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 使用AK&SK初始化账号Client
        /// </summary>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Sts20150401.Client CreateStsClient(string accessKeyId, string accessKeySecret, string endpoint)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                AccessKeyId = accessKeyId,
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = endpoint;
            return new AlibabaCloud.SDK.Sts20150401.Client(config);
        }

        public async Task<PutObjectResult> UploadFileAsync(IFormFile file, string key, string bucketName, string endpointName, AssumeRoleResponseBodyCredentials stsToken)
        {
            PutObjectResult result = null;
            await Task.Run(() =>
            {
                var endpoint = Options.GetEndPoint(endpointName);

                var client = GetOssClient(stsToken.AccessKeyId, stsToken.AccessKeySecret, endpoint.Oss);

                using (var fs = file.OpenReadStream())
                {
                    ObjectMetadata objectMetadata = new ObjectMetadata();
                    objectMetadata.AddHeader("x-oss-security-token", stsToken.SecurityToken);

                    SetContentType(key, objectMetadata);
                    var request = new PutObjectRequest(bucketName, key, fs, objectMetadata);
                    result = client.PutObject(request);
                }
            });
            return result;
        }
        public async Task<PutObjectResult> UploadFileAsync(Stream fs, string key, string bucketName, string endpointName, AssumeRoleResponseBodyCredentials stsToken)
        {
            PutObjectResult result = null;
            await Task.Run(() =>
            {
                var endpoint = Options.GetEndPoint(endpointName);

                var client = GetOssClient(stsToken.AccessKeyId, stsToken.AccessKeySecret, endpoint.Oss);

                using (fs)
                {
                    ObjectMetadata objectMetadata = new ObjectMetadata();
                    objectMetadata.AddHeader("x-oss-security-token", stsToken.SecurityToken);

                    SetContentType(key, objectMetadata);
                    var request = new PutObjectRequest(bucketName, key, fs, objectMetadata);
                    result = client.PutObject(request);
                }
            });
            return result;
        }

        void SetContentType(string key, ObjectMetadata objectMetadata)
        {
            var ext = System.IO.Path.GetExtension(key).ToLower();
            if (ext == ".bmp")
            {
                objectMetadata.ContentType = "image/bmp";
            }
            if (ext == ".gif")
            {
                objectMetadata.ContentType = "image/gif";
            }
            if (ext == ".jpeg"||ext==".jpg")
            {
                objectMetadata.ContentType = "image/jpg";
            }
            if (ext == ".png")
            {
                objectMetadata.ContentType = "image/png";
            }
            if (ext == ".html")
            {
                objectMetadata.ContentType = "text/html";
            }
            if (ext == ".txt")
            {
                objectMetadata.ContentType = "text/plain";
            }
            if (ext == ".vsd")
            {
                objectMetadata.ContentType = "application/vnd.visio";
            }
            if (ext == ".pptx")
            {
                objectMetadata.ContentType = "application/vnd.ms-powerpoint";
            }
            if (ext == ".docx"||ext==".doc")
            {
                objectMetadata.ContentType = "application/msword";
            }
            if (ext == ".xml")
            {
                objectMetadata.ContentType = "text/xml";
            }
           
        }

        public async Task<DeleteObjectResult> DeleteFileAsync(string bucketName,string key,string endpointName, AssumeRoleResponseBodyCredentials stsToken)
        {
            var endpoint = Options.GetEndPoint(endpointName);

            var client = GetOssClient(stsToken.AccessKeyId, stsToken.AccessKeySecret, endpoint.Oss,stsToken.SecurityToken); 
            return await Task.Run(()=> client.DeleteObject(bucketName, key));
        }

        public Uri GeneratePresignedUri(string bucketName,string key,string endpointName, AssumeRoleResponseBodyCredentials stsToken, int minute = 60 )
        {
            var endpoint = Options.GetEndPoint(endpointName);
            var client = GetOssClient(stsToken.AccessKeyId, stsToken.AccessKeySecret, endpoint.Oss, stsToken.SecurityToken); 
            //创建临时访问的url
            return client.GeneratePresignedUri(bucketName,key,DateTime.Now.AddMinutes(minute),SignHttpMethod.Get);
            
        }
      

        OssClient CurrentOssClient = null;
        OssClient GetOssClient(string accessKeyId,string accessKeySecret,string ossEndpoint)
        {
            // 构造OssClient实例。
            //if(OssClient==null)
            CurrentOssClient = new OssClient(ossEndpoint, accessKeyId, accessKeySecret);
            return CurrentOssClient;
        }

        OssClient GetOssClient(string accessKeyId, string accessKeySecret, string ossEndpoint,string securityToken)
        {
            // 构造OssClient实例。
            //if(OssClient==null)
            CurrentOssClient = new OssClient(ossEndpoint, accessKeyId, accessKeySecret, securityToken);
            return CurrentOssClient;
        }
        
        public async Task<string> GetImgUrl(string key,string sessionName,string bucketName="rswl-resource",string endpointName="cn-shanghai")
        {
            if(key.StartsWith("http"))
            {
                return key; 
            }
            

             var stsToken = await GetStsTokenAsync(sessionName, endpointName);
            var uri = GeneratePresignedUri(bucketName, key, endpointName, stsToken.Credentials);
            return uri.ToString();
        }
    }
}
