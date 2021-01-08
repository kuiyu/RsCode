/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 更新文件生命周期
    /// <see cref="https://developer.qiniu.com/kodo/1732/update-file-lifecycle"/>
    /// </summary>
    public class DeleteAfterDaysRequest:QiniuStorageRequest
    {
        public DeleteAfterDaysRequest(string key,int deleteAfterDays)
        {
            EncodedEntryURI =Core.Base64.UrlSafeBase64Encode( key);
            DeleteAfterDays = deleteAfterDays;
        }
        string EncodedEntryURI;
        int DeleteAfterDays;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/deleteAfterDays/{EncodedEntryURI}/{DeleteAfterDays}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
