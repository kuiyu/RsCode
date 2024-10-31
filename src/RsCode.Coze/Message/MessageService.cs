/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Flurl.Http;

namespace RsCode.Coze
{
    public class MessageService
    {
        string Token = CallContext<string>.GetData("cozeToken");
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public  async Task<CozeResult<MessageObject>> CreateAsync(string conversationId,MessageCreateRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/conversation/message/create?conversation_id={conversationId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(request);
            return await res.GetJsonAsync<CozeResult<MessageObject>>();
        }
        /// <summary>
        /// 查看消息列表
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public  async Task<MessageListResponse> ListAsync(string conversationId,MessageListRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/conversation/message/list?conversation_id={conversationId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(request);
            return await res.GetJsonAsync<MessageListResponse> ();
        }

        /// <summary>
        /// 查看消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public  async Task<object> RetrieveAsync(string conversationId,string messageId)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/conversation/message/retrieve?conversation_id={conversationId}&message_id={messageId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync<CozeResult<MessageObject>>();
            return res;
        }

        /// <summary>
        /// 修改一条消息，支持修改消息内容、附加内容和消息类型
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public  async Task<MessageModifyResponse> ModifyAsync(string conversationId, string messageId,MessageModifyRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/conversation/message/modify?conversation_id={conversationId}&message_id={messageId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(request); 
            return await res.GetJsonAsync<MessageModifyResponse>();
        }
    }
}
