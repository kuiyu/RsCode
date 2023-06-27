/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Douyin.Audit
{
    public class ImageAuditRequest:DouyinRequest
    {
        /// <summary>
        ///图片审核
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="accessToken"></param>
        /// <param name="imageUrl"></param>
        public ImageAuditRequest(string appId,string accessToken,string imageUrl=null,string imageData=null)
        {
            AppId = appId;
            AccessToken = accessToken;
            Image = imageUrl;
            ImageData = imageData;
        }
        public override string GetApiUrl()
        {
            return "https://developer.toutiao.com/api/apps/censor/image";
        }

        /// <summary>
        /// 小程序 ID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }
        /// <summary>
        /// 小程序 access_token，参考登录凭证检验
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 检测的图片链接
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }
        /// <summary>
        /// 图片数据的 base64 格式，有 image 字段时，此字段无效
        /// </summary>
        [JsonPropertyName("image_data")]
        public string ImageData { get; set; }
    }
}
