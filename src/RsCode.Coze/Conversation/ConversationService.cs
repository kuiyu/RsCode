/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RsCode.Coze.Core;

namespace RsCode.Coze
{
    /// <summary>
    /// 会话
    /// </summary>
    public class ConversationService
    {
        string Token = CallContext<string>.GetData("cozeToken");
       
         
        /// <summary>
        /// 创建一个会话。
        /// <see cref="https://www.coze.cn/docs/developer_guides/create_conversation#48f17c5f"/>
        /// </summary>
        /// <param name="enterMessage">会话中的消息内容</param>
        /// <param name="metaData">创建消息时的附加消息，获取消息时也会返回此附加消息</param>
        /// <returns></returns>
        public  async Task<CozeResult<ConversationObject>> CreateAsync(EnterMessageObject[] enterMessage,object metaData=null)
        {
            string url = $"https://api.coze.cn/v1/conversation/create"; Token = CallContext<string>.GetData("cozeToken");
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostJsonAsync(new {
                    messages=enterMessage,
                    meta_data=metaData
               });

            return await res.GetJsonAsync<CozeResult<ConversationObject>>();
        }

        public  async Task<object> GetOnlineInfoAsync(string conversationId)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/conversation/retrieve?conversation_id={conversationId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync<CozeResult<ConversationObject>>();

            return res;
        }
    }
}
