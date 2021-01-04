namespace RsCode.Storage.QiniuStorage
{
    public class QiniuStorageRequest:StorageRequest
    {
       
        public virtual TokenType GetTokenType()
        {
            return TokenType.Download;
        }
    }
}
