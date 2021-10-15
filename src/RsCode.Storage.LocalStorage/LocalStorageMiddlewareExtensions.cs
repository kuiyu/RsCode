/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RsCode.Storage.LocalStorage;

namespace RsCode.Storage
{
    public static class LocalStorageMiddlewareExtensions
    {
        public static void UseLocalStorage(this IApplicationBuilder app,IConfiguration configuration)
        {
            //获取token地址
            var tokenUrl = configuration.GetValue<string>("LocalStorage:UploadTokenUrl");
            app.UseWhen(context =>
            {
                return context.Request.Path.Value.StartsWith(tokenUrl);
            },m=>m.UseMiddleware<LocalStorageTokenMiddleware>());
          
            //上传地址
            var uploadUrl = configuration.GetValue<string>("LocalStorage:UploadUrl");
            app.UseWhen(context =>
            {
                return context.Request.Path.Value.StartsWith(uploadUrl);
            }, m => m.UseMiddleware<LocalStorageMiddleware>()
            );

            //访问指定地址的资源
            var accessPath = configuration.GetValue<string>("LocalStorage:AccessPath");
            app.UseWhen(context =>
            {
                return context.Request.Path.Value.StartsWith(accessPath);
            },m=>m.UseMiddleware<FileProviderMiddleware>());


        }
    }
}
