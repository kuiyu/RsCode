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
    public class SpaceRequest:StorageRequest
    {
        public SpaceRequest(string bucket,string beginTime,string endTime,Region region,string g="day" )
        {
            Bucket = bucket;
            BeginTime = beginTime;
            EndTime = endTime;
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
            return $"{Config.DefaultApiHost}/v6/space?begin={BeginTime}&end={EndTime}&g={G}&bucket={Bucket}&region={Region}";
        }
    }
}
