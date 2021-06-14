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
using PetaPoco;
using RsCode.Threading;
using System.Collections.Generic;
using System.Linq;

namespace RsCode.Domain.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : IApplicationDbContext
    { 
        
        IEnumerable<IDatabase> databases;
        IConfiguration Configuration { get; }
        /// <summary>
        /// 当前数据库连接
        /// </summary>
        public IDatabase Current { get;  set; } 
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <param name="_databases"></param>
        public ApplicationDbContext(IEnumerable<IDatabase> _databases)
        { 
            databases = _databases;
           
            Configuration =new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            Current = GetDatabase();
        }

        /// <summary>
        /// 指定数据库连接字符串key,获取数据库配置
        /// </summary>
        /// <param name="connName">默认值为DefaultConnection</param>
        /// <returns></returns>
        public virtual IDatabase GetDatabase(string connName = "DefaultConnection")
        { 
            var connStr = Configuration.GetConnectionString(connName);
            if (string.IsNullOrWhiteSpace(connStr)) throw new System.Exception("ConnectionString not fund");
             
            var db = CallContext<IDatabase>.GetData(connStr);
            if (db == null)
            {              
                db = databases.FirstOrDefault(x => x.ConnectionString == connStr);
            }
            if(db==null)
                throw new System.Exception("ConnectionString not fund");
           
            CallContext<IDatabase>.SetData(connStr, db);       
           
            Current = db;
            return db;
        }

       
    }
}
