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
    public class SpaceRequest: QiniuStorageRequest
    {
        public SpaceRequest(string bucket,DateTime beginTime,DateTime endTime,Region region,string g="day" )
        {
            Bucket = bucket;
            BeginTime = beginTime.ToString("yyyyMMddHHmmss");
            EndTime = endTime.ToString("yyyyMMddHHmmss");
            G = g;
            Region = region.ToDescription();
        }
        string Bucket;
        string BeginTime;
        string EndTime;
        string G;
        string Region;
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.ApiHost;
            return $"{url}/v6/space?begin={BeginTime}&end={EndTime}&g={G}&bucket={Bucket}&region={Region}";
        }

        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
        public override string RequestMethod()
        {
            return "GET";
        }
       
    }
}
