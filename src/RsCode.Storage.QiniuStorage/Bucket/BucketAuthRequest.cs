/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using AspectCore.DependencyInjection;
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 设置 Bucket 访问权限
    /// <see cref="https://developer.qiniu.com/kodo/3946/set-bucket-private"/>
    /// </summary>
    public class BucketAuthRequest:StorageRequest
    { 
        [FromServiceContext]
        public IZoneHelper zoneHelper { get; set; }
        /// <summary>
        /// 设置 Bucket 访问权限
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="_private">0公开 1私有</param>
        public BucketAuthRequest(string bucket, int _private)
        {
           
            BucketName = bucket;
            Private = _private;
        }
        int Private;
          string BucketName { get; set; }

    

        public override string GetApiUrl()
        {
            var zone = zoneHelper.QueryZoneAsync(BucketName).GetAwaiter().GetResult();
            string apiUrl = zone.ApiHost;
            return $"{Config.DefaultApiHost}/private?bucket={BucketName}&private={Private}";
        }
    }
}
