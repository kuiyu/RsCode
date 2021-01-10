using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage
{
    public class StorageMiddleware
    {
        RequestDelegate next;
        IEnumerable<IStorageProvider> provider;
        public StorageMiddleware(RequestDelegate _next,IEnumerable<IStorageProvider> _provider)
        {
            next = _next;
            provider = _provider;
        }

        public async Task Invoke(HttpContext context)
        {
            
            var storageName = context.Request.Query["provider"];
            var action = context.Request.Query["action"];
            if (!string.IsNullOrWhiteSpace(storageName) && !string.IsNullOrWhiteSpace(action))
            {
                storageName = storageName.ToString().ToLower();
                action = action.ToString().ToLower();
                var storageProvider = provider.FirstOrDefault(p => p.StorageName == storageName);
 

                if(action=="upload")
                {
                   var res= await storageProvider.UploadAsync();
                    if (!context.Response.HasStarted)
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(res));
                    }
                }
            }
            else
            { 
                  await next(context);
            }
            



        }
    }
}
