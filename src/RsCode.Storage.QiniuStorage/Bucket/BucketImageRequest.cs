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
    /// 设置 Bucket 镜像源
    /// <see cref="https://developer.qiniu.com/kodo/3966/bucket-image-source"/>
    /// </summary>
    public class BucketImageRequest:StorageRequest
    {
        /// <summary>
        /// 需要设定镜像源的目标空间名
        /// </summary>
        public string BucketName { get; set; }
        /// <summary>
        /// 镜像源的访问域名，必须设置为形如http(s)://source.com或http(s)://114.114.114.114的字符串。参数值必须做URL 安全的 Base64 编码。
        /// </summary>
        public string EncodedSrcSiteUrl { get; set; }
        /// <summary>
        /// 回源时使用的Host头部值。参数值必须做URL 安全的 Base64 编码。可以填空字符串 "" 或不包含 /host/<EncodedHost> 部分，表示不额外指定 host
        /// </summary>
        public string EncodedHost { get; set; }
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/image/{BucketName}/from/{EncodedSrcSiteUrl}/host/{EncodedHost}";
        }
    }
}
