/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.Coze
{
    public class ChatSendRequest
    {
        /// <summary>
        /// 要进行会话聊天的 Bot ID。进入 Bot 的 开发页面，开发页面 URL 中 bot 参数后的数字就是 Bot ID。例如https://www.coze.cn/space/341****/bot/73428668*****，bot ID 为73428668*****。确保该 Bot 的所属空间已经生成了访问令牌。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        /// <summary>
        /// 标识当前与 Bot 交互的用户，由使用方在业务系统中自行定义、生成与维护。
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 对话的附加信息。你可以通过此字段传入本次对话中用户的问题。数组长度限制为 100，即最多传入 100 条消息。当 auto_save_history=true 时，additional_messages 会作为消息先添加到会话中，然后作为上下文传给大模型。当 auto_save_history=false 时，additional_messages 只会作为附加信息传给大模型，additional_messages 和模型返回等本次对话的所有消息均不会添加到会话中。消息结构可参考EnterMessage Object，具体示例可参考携带上下文。为了保证模型效果，additional_messages 最后一条消息会作为本次对话的用户输入内容传给模型，所以最后一条消息建议传 role=user 的记录，以免影响模型效果。如果本次对话未指定会话或指定的会话中无消息时，必须通过此参数传入 Bot 用户的问题。
        /// </summary>
        [JsonPropertyName("additional_messages")]
        public object AdditionalMessages { get; set; }

        /// <summary>
        /// 是否启用流式返回。true：采用流式响应。 “流式响应”将模型的实时响应提供给客户端，你可以实时获取服务端返回的对话、消息事件，并在客户端中同步处理、实时展示，也可以在 completed 事件中获取 Bot 最终的回复。false：（默认）采用非流式响应。 “非流式响应”是指响应中仅包含本次对话的状态等元数据。此时应同时开启 auto_save_history，在本次对话处理结束后再查看模型回复等完整响应内容。可以参考以下业务流程：调用发起会话接口，并设置 stream = false，auto_save_history=true，表示使用非流式响应，并记录历史消息。你需要记录会话的 Conversation ID 和 Chat ID，用于后续查看详细信息。定期轮询查看对话详情接口，直到会话状态流转为终态，即 status 为 completed 或 required_action。调用查看对话消息详情接口，查询大模型生成的最终结果。
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }

        /// <summary>
        /// Bot 中定义的变量。在 Bot prompt 中设置变量 {{key}} 后，可以通过该参数传入变量值，同时支持 Jinja2 语法。详细说明可参考变量示例。变量名只支持英文字母和下划线。
        /// </summary>
        [JsonPropertyName("custom_variables")]
        public object CustomVariables { get; set; }

        /// <summary>
        /// 是否自动保存历史对话记录：true：（默认）保存此次模型回复结果和模型执行中间结果。false：系统不保存历史对话记录，后续无法查看本次对话的基础信息或消息详情。非流式响应下（stream=false），此参数必须设置为 true，即保存历史对话，否则无法查看对话状态和模型回复。
        /// </summary>
        [JsonPropertyName("auto_save_history")]
        public bool AutoSaveHistory { get; set; }

        /// <summary>
        /// 创建消息时的附加消息，获取消息时也会返回此附加消息。自定义键值对，应指定为 Map 对象格式。长度为 16 对键值对，其中键（key）的长度范围为 1～64 个字符，值（value）的长度范围为 1～512 个字符。
        /// </summary>
        [JsonPropertyName("meta_data")]
        public object MetaData { get; set; }


    }
}
