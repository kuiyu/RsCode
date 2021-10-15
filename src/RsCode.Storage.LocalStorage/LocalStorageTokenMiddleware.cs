using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.LocalStorage
{
    public class LocalStorageTokenMiddleware
    {
        IStorageProvider storageProvider;
        readonly RequestDelegate next;
        public LocalStorageTokenMiddleware(RequestDelegate _next,IEnumerable<IStorageProvider> storageProviders)
        {
            next = _next;
            storageProvider = storageProviders.FirstOrDefault(x => x.StorageName == "local");
        }

        public async Task Invoke(HttpContext context)
        {
            var tokenInfo=await storageProvider.GetUploadTokenInfoAsync("",DateTime.Now.AddMinutes(30));
            await context.Response.WriteAsync(JsonSerializer.Serialize(tokenInfo));
        }
    }
}
