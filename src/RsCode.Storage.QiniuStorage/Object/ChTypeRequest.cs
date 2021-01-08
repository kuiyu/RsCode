/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 修改文件存储类型
    /// <see cref="https://developer.qiniu.com/kodo/3710/chtype"/>
    /// </summary>
    public class ChTypeRequest:QiniuStorageRequest
    {
        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="_type">0 表示标准存储；1 表示低频存储；2 表示归档存储</param>
        public ChTypeRequest(string bucket,string key, int _type)
        {
            Bucket=bucket;
            EncodedEntryURI =Core.Base64.UrlSafeBase64Encode(bucket, key);
            type = _type;

        }
        string EncodedEntryURI;
        int type;
        string Bucket;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/chtype/{EncodedEntryURI}/type/{type}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
