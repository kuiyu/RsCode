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
    /// <summary>
    /// 设置 Bucket 访问权限
    /// <see cref="https://developer.qiniu.com/kodo/3946/set-bucket-private"/>
    /// </summary>
    public class BucketAuthRequest:StorageRequest
    {
        public BucketAuthRequest(string bucket,int _private)
        {
            BucketName= bucket;
            Private = _private;
        }
        int Private;
          string BucketName { get; set; }

        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/private?bucket={BucketName}&private={Private}";
        }
    }
}
