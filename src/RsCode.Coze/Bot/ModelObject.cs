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
    public class ModelObject
    {
        /// <summary>
        /// 模型的唯一标识。
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// 模型名称。
        /// </summary>
        [JsonPropertyName("model_name")]
        public string ModelName { get; set; }


    }
}
