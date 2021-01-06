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

namespace RsCode.Storage.QiniuStorage
{
    public class RsChtypeRequest:QiniuStorageRequest
    {
        public RsChtypeRequest(DateTime beginTime, DateTime endTime, string bucket,Region region, string select = "size", string g = "day")
        {
            Select = select;
            BeginTime = beginTime.ToString("yyyyMMddHHmmss");
            EndTime = endTime.ToString("yyyyMMddHHmmss");
            G = g;
            Region = region.ToDescription();
            Bucket = bucket;
        }
        string Select;
        string BeginTime;
        string EndTime;
        string G;

        string Bucket="";
        string Region ;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ApiHost;
            return $"{url}/v6/count_archive?begin={BeginTime}&end={EndTime}&g={G}&select={Select}";
        }

        public override string RequestMethod()
        {
            return "GET";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
