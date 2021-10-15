using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
        public FileProviderMiddleware(RequestDelegate _next,IOptionsSnapshot<LocalStorageOptions> localStorageOptions)
        {
            next = _next;
            StorageOptions = localStorageOptions.Value;
        }
        public async Task InvokeAsync(HttpContext context )
        {
            await AccessResourceAsync(context);
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

        async Task AccessResourceAsync(HttpContext context)
        {
            string requestFile = context.Request.Path;
            requestFile = requestFile.Replace("/res/", "");
            requestFile = requestFile.Replace("//", "\\");
            string physicsPath =StorageOptions.SavePath ;
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
