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
using RsCode.Storage.LocalStorage;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Storage
{
    public interface IFileProviderMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
    public class FileProviderMiddleware : IFileProviderMiddleware
    {
        readonly RequestDelegate next;
        LocalStorageOptions StorageOptions;
        ILocalStorageAccess localStorageAccessService;
        public FileProviderMiddleware(RequestDelegate _next,IOptionsSnapshot<LocalStorageOptions> localStorageOptions, ILocalStorageAccess _localStorageAccessService)
        {
            next = _next;
            StorageOptions = localStorageOptions.Value;
            localStorageAccessService = _localStorageAccessService;
        }
        public async Task InvokeAsync(HttpContext context )
        {
            await localStorageAccessService.AccessFileAsync(context);
        }
         
        async Task<string> ReadBodyAsync(HttpContext context)
        {
            string result = "";
            var stream = context.Request.Body;
            long? length = context.Request.ContentLength;
            if(length!=null&&length>0)
            {
                var sr = new StreamReader(stream, Encoding.UTF8);
                result = await sr.ReadToEndAsync();
            }
            return result;
        }

        public virtual async Task AccessResourceAsync(HttpContext context)
        {
            string requestFile = context.Request.Path;
            var accessPath = StorageOptions.AccessPath;
            requestFile = requestFile.Replace(accessPath, "");
            requestFile = requestFile.Replace("//", "\\");
            string physicsPath =StorageOptions.SavePath ;
            string file = Path.Combine($"{physicsPath}{requestFile}");
            file = System.Web.HttpUtility.UrlDecode(file);
            Console.WriteLine($"file={file}");
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
