using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage.Object
{
    public class ChStatusRequest:StorageRequest
    {
        public ChStatusRequest(string encodedEntry,int status)
        {
            EncodedEntry= encodedEntry;
            Status = status;
        }
        string EncodedEntry;
        int Status;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/chstatus/{EncodedEntry}/status/{Status}";
        }
    }
}
