/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Domain.Uow;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace RsCode
{
    public static class DbServiceCollectionExtensions
    {
       

        /// <summary>
        /// 添加单个数据库配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dataType">数据库类型</param>
        /// <param name="syncStructure"></param>
        public static void AddDatabase(this IServiceCollection services, FreeSql.DataType dataType, bool syncStructure = false)
        {
            AddDatabase(services, dataType, "DefaultConnection", syncStructure);
        }
        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dataType"></param>
        /// <param name="connName">数据库连接名称</param>
        /// <param name="syncStructure"></param>
        public static void AddDatabase(this IServiceCollection services, FreeSql.DataType dataType,string connName, bool syncStructure = false)
        {
            var provider = services.BuildServiceProvider();
            var Configuration = provider.GetService<IConfiguration>();
            var connString = Configuration.GetConnectionString(connName);

            FreeSqlCloud<string> fsql = new FreeSqlCloud<string>();
            fsql.Register(connName, () => new FreeSqlBuilder()
                  .UseConnectionString(dataType, connString)
                  .UseAutoSyncStructure(syncStructure)
                  .Build());
            services.AddSingleton<IFreeSql>(fsql);
            services.AddSingleton(fsql);
            services.TryAddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dataType">数据库类型</param>
        /// <param name="connName"></param>
        /// <param name="syncStructure"></param>
        /// <param name="exeuting"></param>
        public static void AddDatabase(this IServiceCollection services, FreeSql.DataType dataType, string connName, bool syncStructure, Action<DbCommand> exeuting)
        {
            var provider = services.BuildServiceProvider();
            var Configuration = provider.GetService<IConfiguration>();
            var connString = Configuration.GetConnectionString(connName);

            FreeSqlCloud<string> fsql = new FreeSqlCloud<string>();
            fsql.Register(connName, () => new FreeSqlBuilder()
                 .UseConnectionString(dataType, connString)
                 .UseMonitorCommand(exeuting)
                 .UseAutoSyncStructure(syncStructure)
                 .Build());
            services.AddSingleton<IFreeSql>(fsql);
            services.AddSingleton(fsql);
            services.TryAddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
        
    }

}
