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
    /// <summary>
    /// <see cref="https://developer.open-douyin.com/docs/resource/zh-CN/mini-app/develop/server/content-security/picture-detect-v2"/>
    /// </summary>
    public class ImageAuditResponse:DouyinResponse
    {
        /// <summary>
        /// 检测结果-状态码
        /// </summary>
        [JsonPropertyName("error")]
        public int Error { get; set; }
        /// <summary>
        /// 检测结果-消息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
        /// <summary>
        /// 检测结果-置信度列表
        /// </summary>
        [JsonPropertyName("predicts")]
        public PreDict[] Predits { get; set; }
      
    }

    public class PreDict
    {
        /// <summary>
        /// 检测结果-置信度-模型/标签
        /// </summary>
        [JsonPropertyName("model_name")]
        public string ModelName { get; set; }
        /// <summary>
        /// 检测结果-置信度-结果，当值为 true 时表示检测的图片包含违法违规内容，比如是广告
        /// </summary>
        [JsonPropertyName("hit")]
        public bool Hit { get; set; }
    }


}
