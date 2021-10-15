/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.LocalStorage
{
    public interface ILocalStorageMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
    public class LocalStorageMiddleware:ILocalStorageMiddleware
    { 
       
        readonly RequestDelegate next;
        IStorageProvider storageProvider;
        public LocalStorageMiddleware(RequestDelegate _next,IEnumerable< IStorageProvider> _storageProvider)
        {
            next = _next;
            storageProvider = _storageProvider.FirstOrDefault(x=>x.StorageName=="local");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var result=await storageProvider.UploadAsync(); 
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                 
            }
           
        }

        async Task<UploadData> ReadBodyAsync(HttpContext context)
        {
            UploadData data = null;
            string s = "";
            var stream = context.Request.Body;
            long? length = context.Request.ContentLength;//
            if (length != null && length > 0)
            {
                var sr = new StreamReader(stream, Encoding.UTF8);
                s = await sr.ReadToEndAsync();
            }
            if(!string.IsNullOrWhiteSpace(s))
            {
                data=JsonSerializer.Deserialize<UploadData>(s);
            }
            return data;
        }
    }
}
