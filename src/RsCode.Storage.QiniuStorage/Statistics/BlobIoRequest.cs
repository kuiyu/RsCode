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
   public class BlobIoRequest:StorageRequest
    {
        public BlobIoRequest(string beginTime, string endTime, string bucket,string domain,int fType,string select, string g = "day")
        {
            Select = select;
            BeginTime = beginTime;
            EndTime = endTime;
            G = g;
            Domain = domain;
            FileType = fType;
            Bucket = bucket;
        }
        string Select;
        string BeginTime;
        string EndTime;
        string G;
        string Domain;
        int FileType;
        string Bucket;
        
        public override string GetApiUrl()
        {
            return $"{Config.DefaultApiHost}/v6/count_archive?begin={BeginTime}&end={EndTime}&g={G}&select={Select}";
        }
    }
}
