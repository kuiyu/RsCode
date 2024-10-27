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
    public class ChatObject
    {
        /// <summary>
        /// 对话 ID，即对话的唯一标识。
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// 会话 ID，即会话的唯一标识。
        /// </summary>
        [JsonPropertyName("conversation_id")]
        public string ConversationId { get; set; }

        /// <summary>
        /// 要进行会话聊天的 Bot ID。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        /// <summary>
        /// 对话创建的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// 对话结束的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int CompletedAt { get; set; }

        /// <summary>
        /// 对话失败的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("failed_at")]
        public int FailedAt { get; set; }

        /// <summary>
        /// 创建消息时的附加消息，用于传入使用方的自定义数据，获取消息时也会返回此附加消息。自定义键值对，应指定为 Map 对象格式。长度为 16 对键值对，其中键（key）的长度范围为 1～64 个字符，值（value）的长度范围为 1～512 个字符。
        /// </summary>
        [JsonPropertyName("meta_data")]
        public object MetaData { get; set; }

        /// <summary>
        /// 对话运行异常时，此字段中返回详细的错误信息，包括：Code：错误码。Integer 类型。0 表示成功，其他值表示失败。Msg：错误信息。String 类型。对话正常运行时，此字段返回 null。suggestion 失败不会被标记为运行异常，不计入 last_error。
        /// </summary>
        [JsonPropertyName("last_error")]
        public object LastError { get; set; }

        /// <summary>
        /// 会话的运行状态。取值为：created：会话已创建。in_progress：Bot 正在处理中。completed：Bot 已完成处理，本次会话结束。failed：会话失败。requires_action：会话中断，需要进一步处理。
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// 需要运行的信息详情。
        /// </summary>
        [JsonPropertyName("required_action")]
        public object RequiredAction { get; set; }

        /// <summary>
        /// 额外操作的类型，枚举值为 submit_tool_outputs。
        /// </summary>
        [JsonPropertyName(" type")]
        public string  type {get;set;}

    /// <summary>
    /// 需要提交的结果详情，通过提交接口上传，并可以继续聊天
    /// </summary>
    [JsonPropertyName("submit_tool_outputs")]
    public object submitToolOutputs {get;set;}

/// <summary>
/// 具体上报信息详情。
/// </summary>
[JsonPropertyName(" tool_calls")]
public object[]  toolCalls {get; set;}

/// <summary>
/// 上报运行结果的 ID。
/// </summary>
[JsonPropertyName(" id")]
public string  id {get; set;}

///// <summary>
///// 工具类型，枚举值为 function。
///// </summary>
//[JsonPropertyName(" type")]
//public string  type {get; set;}

/// <summary>
/// 执行方法 function 的定义。
/// </summary>
[JsonPropertyName(" function")]
public object  function {get; set;}

/// <summary>
/// 方法名。
/// </summary>
[JsonPropertyName(" name")]
public string  name {get; set;}

/// <summary>
/// 方法参数。
/// </summary>
[JsonPropertyName(" argument")]
public string  argument {get; set;}

/// <summary>
/// Token 消耗的详细信息。实际的 Token 消耗以对话结束后返回的值为准。
/// </summary>
[JsonPropertyName("usage")]
public object Usage { get; set; }

/// <summary>
/// 本次对话消耗的 Token 总数，包括 input 和 output 部分的消耗。
/// </summary>
[JsonPropertyName("token_count")]
public int tokenCount {get; set;}

/// <summary>
/// output 部分消耗的 Token 总数。
/// </summary>
[JsonPropertyName("output_count")]
public int outputCount {get; set;}

/// <summary>
/// input 部分消耗的 Token 总数。
/// </summary>
[JsonPropertyName("input_count")]
public int inputCount {get; set;}


    }
}
