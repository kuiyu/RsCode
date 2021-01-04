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
        /// <param name="url"></param>
        /// <param name="status">值为数字，0表示启用；1表示禁用</param>
        public ChStatusRequest(string url,int status)
        {
            EncodedEntry = Core.Base64.UrlSafeBase64Encode(url);
            Status = status;
        }
        string EncodedEntry;
        int Status;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/chstatus/{EncodedEntry}/status/{Status}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
