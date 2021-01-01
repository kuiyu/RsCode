/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 资源元信息查询
    /// <see cref="https://developer.qiniu.com/kodo/1308/stat"/>
    /// </summary>
    public class StatRequest:StorageRequest
    {
        public StatRequest(string encodedEntryUrl)
        {
            EncodedEntryURI = encodedEntryUrl;
        }
        string EncodedEntryURI;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/stat/{EncodedEntryURI}";
        }
        public override string RequestMethod()
        {
            return "GET";
        }
    }
}
