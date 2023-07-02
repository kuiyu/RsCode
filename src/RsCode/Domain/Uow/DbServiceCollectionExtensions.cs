/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PetaPoco;
using PetaPoco.Core;
using RsCode.Domain.Uow;
using System;

namespace RsCode
{
    public static class DbServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <typeparam name="TDatabaseProvider"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
		public static void AddDatabase<TDatabaseProvider>(this IServiceCollection services,IConfiguration configuration)
		   where TDatabaseProvider : IProvider
		{
			var connStr = configuration.GetConnectionString("DefaultConnection");
			AddDatabase<TDatabaseProvider>(services, connStr, new RsCodeMapper());
		}
		/// <summary>
		/// 使用默认配置添加数据库
		/// </summary>
		/// <typeparam name="TDatabaseProvider"></typeparam>
		/// <param name="services"></param>
		public static void AddDatabase<TDatabaseProvider>(this IServiceCollection services)
           where TDatabaseProvider : IProvider
        {
            var provider = services.BuildServiceProvider();
            var Configuration = provider.GetService<IConfiguration>();
            var connStr = Configuration.GetConnectionString("DefaultConnection");
            AddDatabase<TDatabaseProvider>(services,connStr, new RsCodeMapper());
        }
        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <typeparam name="TDatabaseProvider"></typeparam>
        /// <param name="services"></param>
        /// <param name="connString">数据库连接字符串</param>
        public static void AddDatabase<TDatabaseProvider>(this IServiceCollection services, string connString,IMapper defaultMapper=null)
           where TDatabaseProvider : IProvider
        {           
            services.AddTransient<IDatabase, Database>(x => new Database<TDatabaseProvider>(connString,defaultMapper));
            services.TryAddTransient<IApplicationDbContext, ApplicationDbContext>();
          
        }
        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func"></param>
        public static void AddDatabase(this IServiceCollection services,Func<Database> func)
       {
            services.AddTransient<IDatabase, Database>(x => func());
            services.TryAddTransient<IApplicationDbContext, ApplicationDbContext>();
            
        }
  
     
        
    }
    /// <summary>
    /// 
    /// </summary>
    public class RsCodeMapper:ConventionMapper
    {
        public RsCodeMapper()
        { 
             InflectTableName =  (inflector, s) => inflector.Pluralise(inflector.Underscore(s));
             //InflectColumnName = (inflector, s) => inflector.Underscore(s);
            //InflectColumnName = (inflector, s) => inflector.Pluralise(s);
        }
    }
}
