using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RsCode.Storage.Aliyun
{
    public class AliyunStorageService : IStorageProvider
    {
        public string StorageName { get; } = "aliyun";

        public string CreateDownloadUrl(string url, int expireInSeconds = 3600)
        {
            throw new NotImplementedException();
        }

        public string GetUploadToken(string key, DateTime expiresTime)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> GetUploadTokenInfoAsync(string key, DateTime expiresTime)
        {
            throw new NotImplementedException();
        }

        public Task<T> SendAsync<T>(StorageRequest request) where T : StorageResponse
        {
            throw new NotImplementedException();
        }

        public Task<(HttpResponseMessage, string)> SendAsync(StorageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UploadResult> UploadAsync()
        {
            throw new NotImplementedException();
        }

        public StorageOptions UseBucket(string bucketName)
        {
            throw new NotImplementedException();
        }

       
    }
}
