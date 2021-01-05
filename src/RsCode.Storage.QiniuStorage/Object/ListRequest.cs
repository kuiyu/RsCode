/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using RsCode.Storage.QiniuStorage.Core;

namespace RsCode.Storage.QiniuStorage
{
    public class ListRequest: QiniuStorageRequest
    {
        public ListRequest(string bucket,string marker="",int limit=1000,string prefix="",string delimiter="")
        {
            Bucket = bucket;
            Marker = marker;
            Limit = limit;
            UrlEncodedPrefix = prefix;
            UrlEncodedDelimiter= delimiter;
        }
        string Bucket;
        string Marker;
        int Limit;
        string UrlEncodedPrefix;
        string UrlEncodedDelimiter;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/list?bucket={Bucket}&marker={Marker}&limit={Limit}&prefix={UrlEncodedPrefix}&delimiter={UrlEncodedDelimiter}";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
