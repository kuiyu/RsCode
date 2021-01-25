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
        /// 
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

        public static void AddDatabase(this IServiceCollection services,Func<Database> func)
       {
            services.AddTransient<IDatabase, Database>(x => func());
            services.TryAddTransient<IApplicationDbContext, ApplicationDbContext>();
            
        }
  
        
    }

    public class RsCodeMapper:ConventionMapper
    {
        public RsCodeMapper()
        { 
             InflectTableName =  (inflector, s) => inflector.Pluralise(inflector.Underscore(s));
            InflectColumnName = (inflector, s) => inflector.Underscore(s);
            //InflectColumnName = (inflector, s) => inflector.Pluralise(s);
        }
    }
}
