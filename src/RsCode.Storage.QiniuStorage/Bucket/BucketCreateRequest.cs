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
    /// <summary>
    /// 本接口用于创建一个新的 Bucket。此接口不支持匿名请求。您可以在请求参数中指定存储区域，例如，您在华东，选择华东存储区域可以减少延迟、降低成本。
    /// <see cref="https://developer.qiniu.com/kodo/1382/mkbucketv3"/>
    /// </summary>
    public class BucketCreateRequest:StorageRequest
    {
        public BucketCreateRequest(string bucket,Region region = Region.z0)
        {
            BucketName = bucket;
            Region = region;
        }
         string BucketName { get; set; }
        /// <summary>
        /// 存储区域，默认华东
        /// </summary>
         Region Region { get; set; } = Region.z0;

        public override string GetApiUrl()
        {
            var url= $"{Config.DefaultRsHost}/mkbucketv3/{BucketName}/region/{Region.ToDescription()}";
            return url;
        }
    }
}
