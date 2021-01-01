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
    /// 修改文件存储类型
    /// <see cref="https://developer.qiniu.com/kodo/3710/chtype"/>
    /// </summary>
    public class ChTypeRequest:StorageRequest
    {
        public ChTypeRequest(string encodedEntryURI, int _type)
        {
            EncodedEntryURI = encodedEntryURI;
            type = _type;
        }
        string EncodedEntryURI;
        int type;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/chtype/{EncodedEntryURI}/type/{type}";
        }
    }
}
