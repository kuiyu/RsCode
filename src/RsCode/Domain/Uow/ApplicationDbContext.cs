using Microsoft.Extensions.Configuration;
using PetaPoco;
using RsCode.Threading;
using System.Collections.Generic;
using System.Linq;

namespace RsCode.Domain.Uow
{
    public class ApplicationDbContext : IApplicationDbContext
    { 
        
        IEnumerable<IDatabase> databases;
        IConfiguration Configuration { get; }
        public IDatabase Current { get; set; } 

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
            if (db != null)
            {
                CallContext<IDatabase>.SetData(connStr, db);       
            }
            Current = db;
            return db;
        }

       
    }
}
