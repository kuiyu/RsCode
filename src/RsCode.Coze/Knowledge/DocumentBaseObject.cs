/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class DocumentBaseObject
    {
        /// <summary>
        /// 文件名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 文件的元数据信息。详细信息可参考 SourceInfo object。
        /// </summary>
        [JsonPropertyName("source_info")]
        public SourceInfoObject SourceInfo { get; set; }

        /// <summary>
        /// 在线网页的更新策略。默认不自动更新。详细信息可参考 UpdateRule object。
        /// </summary>
        [JsonPropertyName("update_rule")]
        public UpdateRuleObject UpdateRule { get; set; }


    }
}
