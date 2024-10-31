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

namespace RsCode.Coze
{
    public class KnowledgeService
    {
        string Token = CallContext<string>.GetData("cozeToken");
        public  async Task<DocumentInfoObject> CreateDocumentAsync(DocumentCreateRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
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
        public  async Task<DocumentUpdateResponse> UpdateDocumentAsync(DocumentUpdateRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/open_api/knowledge/document/update";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentUpdateResponse>();
        }

        public  async Task<DocumentDeleteResponse> DeleteDocumentAsync(DocumentDeleteRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/open_api/knowledge/document/delete";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentDeleteResponse>();
        }
        public  async Task<DocumentListResponse> ListDocumentAsync(DocumentListRequest request)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/open_api/knowledge/document/list";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .WithHeader("Agw-Js-Conv", "")
                .PostJsonAsync(request);

            return await res.GetJsonAsync<DocumentListResponse>();
        }


    }
}
