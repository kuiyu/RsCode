/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Flurl.Http;
using Microsoft.Extensions.Options;
using RsCode.Coze.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Coze
{
    /// <summary>
    /// 对话
    /// <see cref="https://www.coze.cn/docs/developer_guides/chat_v3"/>
    /// </summary>
    public class ChatService
    {
        string Token = CallContext<string>.GetData("cozeToken");
        /// <summary>
        /// 调用此接口发起一次对话，支持添加上下文和流式响应
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public  async Task<CozeResult<ChatObject>> SendAsync(string conversationId,ChatSendRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v3/chat?conversation_id={conversationId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(request);
            var s =await res.GetStringAsync();
            return await res.GetJsonAsync<CozeResult<ChatObject>>();
        }
        /// <summary>
        /// 查询对话详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public  async Task<CozeResult<ChatObject>> RetrieveAsync(string token,string conversationId,string chatId)
        {
            string url = $"https://api.coze.cn/v3/chat/retrieve?conversation_id={conversationId}&chat_id={chatId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {token}")
                .GetJsonAsync<CozeResult<ChatObject>>();
          
            return res;
        }
        /// <summary>
        /// 查看对话消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public  async Task<CozeResult<MessageObject[]>> MessageListAsync(string conversationId, string chatId)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v3/chat/message/list?conversation_id={conversationId}&chat_id={chatId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync < CozeResult <MessageObject[]>>();
         
            return res;
        }

        /// <summary>
        /// 提交工具执行结果
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public  async Task<object>SubmitToolOutputAsync(string conversationId,string chatId,ToolOutputRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v3/chat/submit_tool_outputs?conversation_id={conversationId}&chat_id={chatId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(request);
            
            return await res.GetJsonAsync<object>();
        }
    }
}
