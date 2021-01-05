using RsCode.Storage.QiniuStorage.Core;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 修改文件状态
    /// <see cref="https://developer.qiniu.com/kodo/4173/modify-the-file-status"/>
    /// </summary>
    public class ChStatusRequest:QiniuStorageRequest
    {
        /// <summary>
        /// 修改文件状态
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="status">值为数字，0表示启用；1表示禁用</param>
        public ChStatusRequest(string bucket,string key,int status)
        {
            Bucket = bucket;
            EncodedEntry = Core.Base64.UrlSafeBase64Encode(bucket,key);
            Status = status;
        }
        string EncodedEntry;
        int Status;
        string Bucket;
        public override string GetApiUrl()
        {
            var zone=new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/chstatus/{EncodedEntry}/status/{Status}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
