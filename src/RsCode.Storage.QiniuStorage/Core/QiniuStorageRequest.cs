using System.Net.Http;

namespace RsCode.Storage.QiniuStorage
{
    public class QiniuStorageRequest:StorageRequest
    {
       
        public virtual TokenType GetTokenType()
        {
            return TokenType.Download;
        }

        public virtual string ContentType()
        {
            return "application/json";
        }

        public virtual FormUrlEncodedContent FormContent()
        {
            return null;
        }
    }
}
