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
    /// <summary>
    /// 文件
    /// <see cref="https://www.coze.cn/docs/developer_guides/retrieve_files"/>
    /// </summary>
    public class FilesService
    {
        string Token = CallContext<string>.GetData("cozeToken");
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public  async Task<object> UploadAsync()
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/files/upload";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .PostAsync();

            return await res.GetJsonAsync<CozeResult<FileObject>>();
        }

        public  async Task<object>RetrieveAsync(string fileId)
        {
            Token = CallContext<string>.GetData("cozeToken");
            string url = $"https://api.coze.cn/v1/files/retrieve?file_id={fileId}";
            var res = await url
                .WithHeader($"Authorization", $"Bearer {Token}")
                .GetJsonAsync<CozeResult<FileObject>>();

            return res;
        }
    }
}
