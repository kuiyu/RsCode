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
    public class BucketTagPutRequest:StorageRequest
    {
        public BucketTagPutRequest(string bucket,Dictionary<string,object>tags)
        {
            BucketName = bucket;
            Tags = tags;
        }
        public string BucketName {  get; set; }
        public Dictionary<string,object> Tags { get; set; }

        public override string RequestMethod()
        {
            return "PUT";
        }
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/bucketTagging?bucket={BucketName}";
        }
    }
}
