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
   public class BlobTransferRequest:QiniuStorageRequest
    {
        public BlobTransferRequest(string bucket, string beginTime, string endTime ,bool isOverSea,string taskId,string select="size",string g = "day")
        {
            Bucket = bucket;
            Select = select;
            BeginTime = beginTime;
            EndTime = endTime;
            G = g;
            IsOverSea = isOverSea;
            TaskId = taskId;
        }
        string Bucket;
        string Select;
        string BeginTime;
        string EndTime;
        string G;
        
        bool IsOverSea = false;
        string TaskId = "";
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
