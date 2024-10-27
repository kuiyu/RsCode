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
    /// <summary>
    /// Bot信息
    /// </summary>
    public class BotObject:CozeBaseInfo
    {
        /// <summary>
        /// Bot 的唯一标识。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        /// <summary>
        /// Bot 的名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Bot 的描述信息。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Bot 的头像地址。
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 创建时间，格式为 10 位的 Unixtime 时间戳，单位为秒（s）。
        /// </summary>
        [JsonPropertyName("create_time")]
        public int CreateTime { get; set; }

        /// <summary>
        /// 更新时间，格式为 10 位的 Unixtime 时间戳，单位为秒（s）。
        /// </summary>
        [JsonPropertyName("update_time")]
        public int UpdateTime { get; set; }

        /// <summary>
        /// Bot 最新版本的版本号。
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Bot 的提示词配置，参考 Prompt object 说明。
        /// </summary>
        [JsonPropertyName("prompt_info")]
        public PromptObject PromptInfo { get; set; }

        /// <summary>
        /// Bot 的开场白配置，参考 Onboarding object 说明。
        /// </summary>
        [JsonPropertyName("onboarding_info")]
        public OnboardingObject OnboardingInfo { get; set; }

        /// <summary>
        /// Bot 模式，取值：0：单 Agent 模式1：多 Agent 模式
        /// </summary>
        [JsonPropertyName("bot_mode")]
        public int BotMode { get; set; }

        /// <summary>
        /// Bot 配置的插件，参考 Plugin object 说明。
        /// </summary>
        [JsonPropertyName("plugin_info_list")]
        public PluginObject[] PluginInfoList { get; set; }

        /// <summary>
        /// Bot 配置的模型，参考 Model object 说明。
        /// </summary>
        [JsonPropertyName("model_info")]
        public ModelObject ModelInfo { get; set; }





    }
}
