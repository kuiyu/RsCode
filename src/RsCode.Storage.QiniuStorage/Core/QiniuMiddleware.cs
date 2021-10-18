using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage.Core
{
    public class QiniuMiddleware
    {
        readonly RequestDelegate next; 
        IStorageProvider storageProvider;
        public QiniuMiddleware(RequestDelegate _next, IEnumerable<IStorageProvider> _storageProvider)
        {
            next = _next;
            storageProvider = _storageProvider.FirstOrDefault(x => x.StorageName == "qiniu");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var result = await storageProvider.UploadAsync();
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {

            }

        }
    }
}
