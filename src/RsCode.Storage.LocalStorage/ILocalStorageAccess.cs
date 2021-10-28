/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace RsCode.Storage.LocalStorage
{
    public interface ILocalStorageAccess
    {
        Task AccessFileAsync(HttpContext context);
    }

    public  class LocalStorageAccess : ILocalStorageAccess
    {
        LocalStorageOptions StorageOptions;


        public LocalStorageAccess(IOptionsSnapshot<LocalStorageOptions> localStorageOptions)
        {
            StorageOptions = localStorageOptions.Value;
        }
        public virtual async Task AccessFileAsync(HttpContext context)
        { 
            string requestFile = context.Request.Path;
            var accessPath = StorageOptions.AccessPath;
            requestFile = requestFile.Replace(accessPath, "");
            requestFile = requestFile.Replace("//", "\\");
            string physicsPath = StorageOptions.SavePath;
            string file = Path.Combine($"{physicsPath}{requestFile}");
            file = System.Web.HttpUtility.UrlDecode(file); 
            if (File.Exists(file))
            {
                await context.Response.SendFileAsync(file);
            }
            else
            {
                await context.Response.WriteAsync("文件不存在");
            }
        }
    }
}
