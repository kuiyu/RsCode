﻿/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Storage;
using RsCode.Storage.QiniuStorage;
using RsCode.Storage.QiniuStorage.CDN;

 
    public static class QiniuStorageServiceExtension
    {
        public static void AddQiniuStorage(this IServiceCollection services, IConfiguration configuration,int ExpireSeconds=600)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<QiniuOptions>();
            services.Configure<QiniuOptions>(configuration.GetSection("QiniuStorage"));
            
            services.AddScoped<IStorageProvider, QiniuStorageProvider>();

            services.AddHttpClient<QiniuHttpClient>();

            services.AddScoped<ICdnManager, CdnManager>();
            
        }
    }
 