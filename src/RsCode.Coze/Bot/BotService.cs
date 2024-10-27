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
    /// <summary>
    /// Bot
    /// <see cref="https://www.coze.cn/docs/developer_guides/get_metadata"/>
    /// </summary>
    public class BotService:CozeServiceBase
    {
        /// <summary>
        /// 获取指定 Bot 的配置信息。
        /// </summary>
        /// <param name="botId"></param>
        /// <returns></returns>
        public static async Task<object> GetOnlineInfoAsync(string botId)
        { 
            string url = $"https://api.coze.cn/v1/bot/get_online_info?bot_id={botId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync<CozeResult<BotObject>>();

            return res;
        }
        /// <summary>
        /// 查看指定空间发布到 Bot as API 渠道的 Bot 列表。
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public static async Task<object> PublishedOnlineListAsync(string spaceId,int page,int pageSize)
        {
            string url = $"https://api.coze.cn/v1/space/published_bots_list?space_id={spaceId}&page_size={pageSize}&page_index={page}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync<CozeResult<SimpleBotObject[]>>();

            return res;
        }
 

    }
}
