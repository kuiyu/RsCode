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
using PetaPoco;
using PetaPoco.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsCode.Domain.Uow
{
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationDbContext : IApplicationDbContext
    {
        IEnumerable<IDatabase> databases;
        IServiceProvider serviceProvider;
        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <param name="databases"></param>
        /// <param name="serviceProvider"></param>
        public ApplicationDbContext(IEnumerable<IDatabase> databases, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.databases = databases;
        }


        IDatabase db;
        /// <summary>
        /// 当前数据库连接
        /// </summary>
        public IDatabase Current
        {
            get
            {
				db = GetDatabase();
				return db;
            }
            private set
            {
                db=value;
            }
        }


        /// <summary>
        /// 指定数据库连接字符串key,获取数据库配置
        /// </summary>
        /// <param name="connName">默认值为DefaultConnection</param>
        /// <returns></returns>
        public virtual IDatabase GetDatabase(string connName = "DefaultConnection")
        {
            var connStr = serviceProvider.GetService<IConfiguration>().GetConnectionString(connName);
            if (string.IsNullOrWhiteSpace(connStr)) throw new System.Exception("ConnectionString not fund");


             db = CallContext<IDatabase>.GetData(connName);
            if (db == null)
            {
                db = databases.FirstOrDefault(x => x.ConnectionString == connStr);
            }
            if (db == null)
                throw new System.Exception("ConnectionString not fund");

			if (db.Connection == null)
			{
				db.OpenSharedConnection();
			}

			CallContext<IDatabase>.SetData(connName, db);

            return db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync<T>(object primaryKeyValue)
        {
            var pd = PocoData.ForType(typeof(T), Current.DefaultMapper);
            string key = pd.TableInfo.PrimaryKey;
            string sql = $"where {key}={primaryKeyValue}";
            return await Current.FirstOrDefaultAsync<T>(sql);
        }

        public virtual async Task<Page<T>> GetPageAsync<T>(int page, int rows)
        {
            return await Current.PageAsync<T>(page, rows);
        }

        /// <summary>
        /// 新增记录,返回主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<object> InsertAsync<T>(T t)
        {
            return await Current.InsertAsync(t);
        }
        /// <summary>
        /// 保存记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task SaveAsync<T>(T t)
        {
            await Current.SaveAsync(t);
        }

        public virtual async Task<int> DeleteAsync<T>(object primaryKeyValue)
        {
            var pd = PocoData.ForType(typeof(T), Current.DefaultMapper);
            string key = pd.TableInfo.PrimaryKey;
            string sql = $"where {key}={primaryKeyValue}";
            return await Current.DeleteAsync<T>(sql);
        }



        //string GetPrimaryKeyValue()
        //{
        //    var pkAttr = typeof(TEntity).GetCustomAttributes(typeof(PrimaryKeyAttribute), true).FirstOrDefault() as PrimaryKeyAttribute;
        //    return pkAttr?.Value;
        //}


    }
}
