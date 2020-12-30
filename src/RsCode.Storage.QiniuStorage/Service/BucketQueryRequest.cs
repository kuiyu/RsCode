/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 获取Bucket列表
    /// <see cref="https://developer.qiniu.com/kodo/3926/get-service#4"/>
    /// </summary>
    public class BucketQueryRequest:StorageRequest
    {
        public BucketQueryRequest(string tags="")
        {
            Encodedtags = tags;
        }
        /// <summary>
        /// 过滤空间的标签或标签值条件
        /// </summary>
        public string Encodedtags { get; set; }
        public override string GetApiUrl()
        {
            string url= "/buckets";
            if (!string.IsNullOrWhiteSpace(Encodedtags))
            {
              
            }
            return url;
        }
        public override string RequestMethod()
        {
            return "GET";
        }
    }
}
