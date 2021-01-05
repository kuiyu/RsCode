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
    /// 本接口用于删除指定的 Bucket。
    /// <see cref="https://developer.qiniu.com/kodo/1601/drop-bucket"/>
    /// </summary>
    public class BucketRemoveRequest:QiniuStorageRequest
    {
        public BucketRemoveRequest(string bucket)
        {
            BucketName = bucket;
        }
         string BucketName { get; set; }
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/drop/{BucketName}";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
