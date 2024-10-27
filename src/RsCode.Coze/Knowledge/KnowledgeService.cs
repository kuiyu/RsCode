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
    public class KnowledgeService:CozeServiceBase
    {
        public static async Task<DocumentInfoObject> CreateDocumentAsync(DocumentCreateRequest request)
        {
            string url = $"https://api.coze.cn/open_api/knowledge/document/create";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv","")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentInfoObject>();
        }
        /// <summary>
        /// 修改知识库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<DocumentUpdateResponse> UpdateDocumentAsync(DocumentUpdateRequest request)
        {
            string url = $"https://api.coze.cn/open_api/knowledge/document/update";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentUpdateResponse>();
        }

        public static async Task<DocumentDeleteResponse> DeleteDocumentAsync(DocumentDeleteRequest request)
        {
            string url = $"https://api.coze.cn/open_api/knowledge/document/delete";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentDeleteResponse>();
        }
        public static async Task<DocumentListResponse> ListDocumentAsync(DocumentListRequest request)
        {
            string url = $"https://api.coze.cn/open_api/knowledge/document/list";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentListResponse>();
        }


    }
}
