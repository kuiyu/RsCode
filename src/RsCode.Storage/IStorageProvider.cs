using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        /// <param name="IsClient">是否是客户端上传</param>
        /// <returns></returns>
        TokenResult GetUploadToken(bool isClient = true);

        /// <summary>
        /// 获取下载凭证
        /// </summary>
        /// <param name="Bucket"></param>
        /// <returns></returns>
        TokenResult GetDownloadToken(bool isClient = true);


        /// <summary>
        /// 获取管理凭证
        /// </summary>
        /// <param name="Bucket"></param>
        /// <returns></returns>
        TokenResult GetManageToken(bool isClient = true);


        Task<UploadResult> UploadAsync();

        Task<T> SendAsync<T>(StorageRequest request) where T : StorageResponse;


        Task<HttpResponseMessage> SendAsync(StorageRequest request);
    }
}
