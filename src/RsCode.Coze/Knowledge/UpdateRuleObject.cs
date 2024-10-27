/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class UpdateRuleObject
    {
        /// <summary>
        /// 在线网页是否自动更新。取值包括：0：不自动更新1：自动更新
        /// </summary>
        [JsonPropertyName("update_type")]
        public int UpdateType { get; set; }

        /// <summary>
        /// 在线网页自动更新的频率。单位为小时，最小值为 24。
        /// </summary>
        [JsonPropertyName("update_interval")]
        public int UpdateInterval { get; set; }


    }
}
