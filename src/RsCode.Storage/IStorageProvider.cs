/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RsCode.Storage
{
    public interface IStorageProvider
    {
        /// <summary>
        /// 云存储名称
        /// </summary>
        string StorageName { get; }
        /// <summary>
        /// 获取上传token
        /// </summary>
        /// <param name="key">bucket或 bucket:key</param>
        /// <param name="expiresTime"></param>
        /// <returns></returns>
        string GetUploadToken(string key, DateTime expiresTime);


        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="expireInSeconds"></param>
        /// <returns></returns>
        string CreateDownloadUrl(string url, int expireInSeconds = 3600);



        Task<TokenResult> GetUploadTokenInfoAsync(string key, DateTime expiresTime);


        Task<UploadResult> UploadAsync();

        StorageOptions UseBucket(string bucket);

        Task<T> SendAsync<T>(StorageRequest request) where T : StorageResponse;


        Task<(HttpResponseMessage,string)> SendAsync(StorageRequest request);

        Task<UploadResult> UploadAsync(Stream stream, string key, string token);
    }
}
