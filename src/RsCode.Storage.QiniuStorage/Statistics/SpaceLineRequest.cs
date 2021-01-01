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
    public class SpaceLineRequest:StorageRequest
    {
        /// <summary>
        /// 该接口可以获取低频存储的当前存储量。可查询当天计量，统计延迟大概 5 分钟。
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="region"></param>
        /// <param name="g"></param>
        public SpaceLineRequest(string bucket, string beginTime, string endTime, Region region, string g = "day")
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
        int NoPredel = 1;
        int OnlyPredel = 1;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/v6/space_line?begin={BeginTime}&end={EndTime}&g={G}&bucket={Bucket}&region={Region}";
        }
    }
}
