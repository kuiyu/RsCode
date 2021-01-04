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
    public class BucketTaggingRequest:StorageRequest
    {
        public BucketTaggingRequest(string bucket)
        {
            BucketName = bucket;
        }
        public string BucketName { get; set; }

        public override string GetApiUrl()
        {
            var zone=new ZoneHelper().QueryZoneAsync(BucketName).GetAwaiter().GetResult();
            var apiUrl =zone.UcHost;
            return $"{apiUrl}/bucketTagging?bucket={BucketName}";
        }
        public override string RequestMethod()
        {
            return "GET";
        }
    }
}
