/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：河南软商网络科技有限公司
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using AspectCore.DependencyInjection;
using PetaPoco;
using PetaPoco.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Domain
{


    public class Repository<T>:Repository<T,long>, IRepository<T> where T : IEntity<long>
    {
      
    }
    public  class Repository<T, TPrimaryKey> : IRepository<T,TPrimaryKey> where T : IEntity<TPrimaryKey>
    {
        [FromServiceContext]
        public IApplicationDbContext applicationDbContext { get; set; }
      

        protected IDatabase db
        {
            get
            {
                return applicationDbContext.Current;
            }
        }

       

        public virtual IDatabase ChangeDataBase(string connStrName)
        {
            applicationDbContext.GetDatabase(connStrName);
            return db;
        }
        
        public virtual int Delete(T poco)
        {
            return db.Delete<T>(poco);
        }

        public virtual int Delete(TPrimaryKey primaryKey)
        {
            return db.Delete<T>($"where {GetPrimaryKey()}=@0",new object[] { primaryKey});
        }

        public virtual int Delete(string sql, params object[] args)
        {
            return db.Delete<T>(sql, args);
        }

        public virtual int Delete(Sql sql)
        {
            return db.Delete<T>(sql);
        }

        public virtual async Task<int> DeleteAsync(T poco)
        {
            return await db.DeleteAsync<T>(poco);
        }
        public virtual async Task<int> DeleteAsync(TPrimaryKey  primaryKey)
        {
            return await db.DeleteAsync<T>($"where {GetPrimaryKey()}=@0",new object[] { primaryKey});
        }
        public virtual async Task<int> DeleteAsync(CancellationToken cancellationToken, T poco)
        {
            return await db.DeleteAsync<T>(cancellationToken, poco);
        }

        public virtual async Task<int> DeleteAsync(string sql, params object[] args)
        {
            return await db.DeleteAsync<T>(sql, args);
        }

        public virtual async Task<int> DeleteAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            return await db.DeleteAsync<T>(cancellationToken, sql, args);
        }

        public virtual async Task<int> DeleteAsync(Sql sql)
        {
            return await db.DeleteAsync<T>(sql);
        }

        public virtual async Task<int> DeleteAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.DeleteAsync<T>(cancellationToken, sql);
        }

        public virtual object ExecuteScalar(string sql, params object[] args)
        {
            var ret= db.ExecuteScalar<object>(sql, args);
            if(!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

        public virtual object ExecuteScalar(Sql sql)
        {
            var ret = db.ExecuteScalar<object>(sql);
            if (!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

        public virtual async Task<object> ExecuteScalarAsync(string sql, params object[] args)
        {
            var ret = await db.ExecuteScalarAsync<object>(sql, args);
            if (!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

        public virtual async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            var ret = await db.ExecuteScalarAsync<object>(cancellationToken, sql, args);
            if (!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

        public virtual async Task<object> ExecuteScalarAsync(Sql sql)
        {
            var ret = await db.ExecuteScalarAsync<object>(sql);
            if (!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

        public virtual async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken, Sql sql)
        {
            var ret= await db.ExecuteScalarAsync<object>(cancellationToken, sql);
            if (!Convert.IsDBNull(ret))
            {
                return ret;
            }
            return null;
        }

         
        public virtual bool Exists(string sqlCondition, params object[] args)
        {
            return db.Exists<T>(sqlCondition, args);
        }

         

         
        public virtual async Task<bool> ExistsAsync(string sqlCondition, params object[] args)
        {
            return await db.ExistsAsync<T>(sqlCondition, args);
        }

        public virtual async Task<bool> ExistsAsync(CancellationToken cancellationToken, string sqlCondition, params object[] args)
        {
            return await db.ExistsAsync<T>(cancellationToken, sqlCondition, args);
        }

        public virtual List<T> Fetch()
        {
            return db.Fetch<T>();
        }

        public virtual List<T> Fetch(string sql, params object[] args)
        {
            return db.Fetch<T>(sql, args);
        }

        public virtual List<T> Fetch(Sql sql)
        {
            return db.Fetch<T>(sql);
        }

        public virtual List<T> Fetch(long page, long itemsPerPage)
        {
            return db.Fetch<T>(page, itemsPerPage);
        }

        public virtual List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args)
        {
            return db.Fetch<T>(page, itemsPerPage, sql, args);
        }

        public virtual List<T> Fetch(long page, long itemsPerPage, Sql sql)
        {
            return db.Fetch<T>(page, itemsPerPage, sql);
        }

        public virtual List<TRet> Fetch<T2, TRet>(Func<T, T2, TRet> cb, string sql, params object[] args)
        {
            return db.Fetch<T, T2, TRet>(cb, sql, args);
        }

        public virtual List<TRet> Fetch<T2, T3, TRet>(Func<T, T2, T3, TRet> cb, string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3, TRet>(cb, sql, args);
        }

        public virtual List<TRet> Fetch<T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3, T4, TRet>(cb, sql, args);
        }

        public virtual List<TRet> Fetch<T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3, T4, T5, TRet>(cb, sql, args);
        }

        public virtual List<TRet> Fetch<T2, TRet>(Func<T, T2, TRet> cb, Sql sql)
        {
            return db.Fetch<T, T2, TRet>(cb, sql);
        }

        public virtual List<TRet> Fetch<T2, T3, TRet>(Func<T, T2, T3, TRet> cb, Sql sql)
        {
            return db.Fetch<T, T2, T3, TRet>(cb, sql);
        }

        public virtual List<TRet> Fetch<T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, Sql sql)
        {
            return db.Fetch<T, T2, T3, T4, TRet>(cb, sql);
        }

        public virtual List<TRet> Fetch<T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, Sql sql)
        {
            return db.Fetch<T, T2, T3, T4, T5, TRet>(cb, sql);
        }

        public virtual List<T> Fetch<T2>(string sql, params object[] args)
        {
            return db.Fetch<T, T2>(sql, args);
        }

        public virtual List<T> Fetch<T2, T3>(string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3>(sql, args);
        }

        public virtual List<T> Fetch<T2, T3, T4>(string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3, T4>(sql, args);
        }

        public virtual List<T> Fetch<T2, T3, T4, T5>(string sql, params object[] args)
        {
            return db.Fetch<T, T2, T3, T4, T5>(sql, args);
        }

        public virtual List<T> Fetch<T2>(Sql sql)
        {
            return db.Fetch<T, T2>(sql);
        }

        public virtual List<T> Fetch<T2, T3>(Sql sql)
        {
            return db.Fetch<T, T2, T3>(sql);
        }

        public virtual List<T> Fetch<T2, T3, T4>(Sql sql)
        {
            return db.Fetch<T, T2, T3, T4>(sql);
        }

        public virtual List<T> Fetch<T2, T3, T4, T5>(Sql sql)
        {
            return db.Fetch<T, T2, T3, T4, T5>(sql);
        }

        public virtual async Task<List<T>> FetchAsync()
        {
            return await db.FetchAsync<T>();
        }

        public virtual async Task<List<T>> FetchAsync(CommandType commandType)
        {
            return await db.FetchAsync<T>(commandType);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken)
        {
            return await db.FetchAsync<T>(cancellationToken);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType)
        {
            return await db.FetchAsync<T>(cancellationToken, commandType);
        }

        public virtual async Task<List<T>> FetchAsync(string sql, params object[] args)
        {
            return await db.FetchAsync<T>(sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(CommandType commandType, string sql, params object[] args)
        {
            return await db.FetchAsync<T>(commandType, sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            return await db.FetchAsync<T>(cancellationToken, sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args)
        {
            return await db.FetchAsync<T>(cancellationToken, commandType, sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(Sql sql)
        {
            return await db.FetchAsync<T>(sql);
        }

        public virtual async Task<List<T>> FetchAsync(CommandType commandType, Sql sql)
        {
            return await db.FetchAsync<T>(commandType, sql);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.FetchAsync<T>(cancellationToken, sql);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType, Sql sql)
        {
            return await db.FetchAsync<T>(cancellationToken, commandType, sql);
        }

        public virtual async Task<List<T>> FetchAsync(long page, long itemsPerPage)
        {
            return await db.FetchAsync<T>(page, itemsPerPage);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage)
        {
            return await db.FetchAsync<T>(cancellationToken, page, itemsPerPage);
        }

        public virtual async Task<List<T>> FetchAsync(long page, long itemsPerPage, string sql, params object[] args)
        {
            return await db.FetchAsync<T>(page, itemsPerPage, sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sql, params object[] args)
        {
            return await db.FetchAsync<T>(cancellationToken, page, itemsPerPage, sql, args);
        }

        public virtual async Task<List<T>> FetchAsync(long page, long itemsPerPage, Sql sql)
        {
            return await db.FetchAsync<T>(page, itemsPerPage, sql);
        }

        public virtual async Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sql)
        {
            return await db.FetchAsync<T>(cancellationToken, page, itemsPerPage, sql);
        }

        public virtual T First(string sql, params object[] args)
        {
            return db.First<T>(sql, args);
        }

        public virtual T First(Sql sql)
        {
            return db.First<T>(sql);
        }

        public virtual async Task<T> FirstAsync(string sql, params object[] args)
        {
            return await db.FirstAsync<T>(sql, args);
        }

        public virtual async Task<T> FirstAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            return await db.FirstAsync<T>(cancellationToken, sql, args);
        }

        public virtual async Task<T> FirstAsync(Sql sql)
        {
            return await db.FirstAsync<T>(sql);
        }

        public virtual async Task<T> FirstAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.FirstAsync<T>(cancellationToken, sql);
        }

        public virtual T FirstOrDefault(string sql, params object[] args)
        {
            return db.FirstOrDefault<T>(sql, args);
        }

        public virtual T FirstOrDefault(Sql sql)
        {
            return db.FirstOrDefault<T>(sql);
        }

        public virtual async Task<T> FirstOrDefaultAsync(string sql, params object[] args)
        {
            return await db.FirstOrDefaultAsync<T>(sql, args);
        }

        public virtual async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            return await db.FirstOrDefaultAsync<T>(cancellationToken, sql, args);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Sql sql)
        {
            return await db.FirstOrDefaultAsync<T>(sql);
        }
        public virtual async Task<T> FirstOrDefaultAsync(TPrimaryKey primaryKey)
        { 
            return await db.FirstOrDefaultAsync<T>($"where {GetPrimaryKey()}=@0",new object[] {primaryKey });
        }
        public virtual async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.FirstOrDefaultAsync<T>(cancellationToken, sql);
        }

        public virtual TPrimaryKey Insert(T poco)
        { 
            var obj= db.Insert(poco);
            if (!Convert.IsDBNull(obj))
            {
                return (TPrimaryKey)Convert.ChangeType( obj,typeof(TPrimaryKey)); 
            }else
            {
              return default(TPrimaryKey);
            } 
        }

        public virtual async Task<TPrimaryKey> InsertAsync(T poco)
        {
            var obj = await db.InsertAsync(poco);
            if (!Convert.IsDBNull(obj))
            {
                return (TPrimaryKey)Convert.ChangeType( obj,typeof(TPrimaryKey)); 
            }
            else
            {
                return default(TPrimaryKey);
            }
        }

        public virtual async Task<TPrimaryKey> InsertAsync(CancellationToken cancellationToken, T poco)
        {
            var obj = await db.InsertAsync(cancellationToken, poco);
            if (!Convert.IsDBNull(obj))
            {
                return (TPrimaryKey)Convert.ChangeType( obj,typeof(TPrimaryKey)); 
            }
            else
            {
                return default(TPrimaryKey);
            }
        }

        

        public virtual Page<T> Page(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs)
        {
            return db.Page<T>(page, itemsPerPage, sqlCount, countArgs, sqlPage, pageArgs);
        }

        public virtual Page<T> Page(long page, long itemsPerPage)
        {
            return db.Page<T>(page, itemsPerPage);
        }

        public virtual Page<T> Page(long page, long itemsPerPage, string sql, params object[] args)
        {
            return db.Page<T>(page, itemsPerPage, sql, args);
        }

        public virtual Page<T> Page(long page, long itemsPerPage, Sql sql)
        {
            return db.Page<T>(page, itemsPerPage, sql);
        }

        public virtual Page<T> Page(long page, long itemsPerPage, Sql sqlCount, Sql sqlPage)
        {
            return db.Page<T>(page, itemsPerPage, sqlCount, sqlPage);
        }

        public virtual async Task<Page<T>> PageAsync(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs)
        {
            return await db.PageAsync<T>(page, itemsPerPage, sqlCount, countArgs, sqlPage, pageArgs);
        }

        public virtual async Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs)
        {
            return await db.PageAsync<T>(cancellationToken, page, itemsPerPage, sqlCount, countArgs, sqlPage, pageArgs);
        }

        public virtual async Task<Page<T>> PageAsync(long page, long itemsPerPage)
        {
            return await db.PageAsync<T>(page, itemsPerPage);
        }

        public virtual async Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage)
        {
            return await db.PageAsync<T>(cancellationToken, page, itemsPerPage);
        }

        public virtual async Task<Page<T>> PageAsync(long page, long itemsPerPage, string sql, params object[] args)
        {
            return await db.PageAsync<T>(page, itemsPerPage, sql, args);
        }

        public virtual async Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sql, params object[] args)
        {
            return await db.PageAsync<T>(cancellationToken, page, itemsPerPage, sql, args);
        }

        public virtual async Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sql)
        {
            return await db.PageAsync<T>(page, itemsPerPage, sql);
        }

        public virtual async Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sql)
        {
            return await db.PageAsync<T>(cancellationToken, page, itemsPerPage, sql);
        }

        public virtual async Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sqlCount, Sql sqlPage)
        {
            return await db.PageAsync<T>(page, itemsPerPage, sqlCount, sqlPage);
        }

        public virtual async Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sqlCount, Sql sqlPage)
        {
            return await db.PageAsync<T>(cancellationToken, page, itemsPerPage, sqlCount, sqlPage);
        }

        public virtual  IEnumerable<T> Query()
        {
            return db.Query<T>();
        }

        public virtual IEnumerable<T> Query(string sql, params object[] args)
        {
            return db.Query<T>(sql, args);
        }

        public virtual IEnumerable<T> Query(Sql sql)
        {
            return db.Query<T>(sql);
        }

        public virtual IEnumerable<TRet> Query<T, T2, TRet>(Func<T, T2, TRet> cb, string sql, params object[] args)
        {
            return db.Query<T, T2, TRet>(cb, sql, args);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, TRet>(Func<T, T2, T3, TRet> cb, string sql, params object[] args)
        {
            return db.Query<T, T2, T3, TRet>(cb, sql, args);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, string sql, params object[] args)
        {
            return db.Query<T, T2, T3, T4, TRet>(cb, sql, args);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, string sql, params object[] args)
        {
            return db.Query<T, T2, T3, T4, T5, TRet>(cb, sql, args);
        }

        public virtual IEnumerable<TRet> Query<T, T2, TRet>(Func<T, T2, TRet> cb, Sql sql)
        {
            return db.Query<T, T2, TRet>(cb, sql);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, TRet>(Func<T, T2, T3, TRet> cb, Sql sql)
        {
            return db.Query<T, T2, T3, TRet>(cb, sql);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, Sql sql)
        {
            return db.Query<T, T2, T3, T4, TRet>(cb, sql);
        }

        public virtual IEnumerable<TRet> Query<T, T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, Sql sql)
        {
            return db.Query<T, T2, T3, T4, T5, TRet>(cb, sql);
        }

        public virtual IEnumerable<T> Query<T, T2>(string sql, params object[] args)
        {
            return db.Query<T, T2>(sql, args);
        }

        public virtual IEnumerable<T> Query<T, T2, T3>(string sql, params object[] args)
        {
            return db.Query<T, T2, T3>(sql, args);
        }

        public virtual IEnumerable<T> Query<T, T2, T3, T4>(string sql, params object[] args)
        {
            return db.Query<T, T2, T3, T4>(sql, args);
        }

        public virtual IEnumerable<T> Query<T, T2, T3, T4, T5>(string sql, params object[] args)
        {
            return db.Query<T, T2, T3, T4, T5>(sql, args);
        }

        public virtual IEnumerable<T> Query<T, T2>(Sql sql)
        {
            return db.Query<T, T2>(sql);
        }

        public virtual IEnumerable<T> Query<T, T2, T3>(Sql sql)
        {
            return db.Query<T, T2, T3>(sql);
        }

        public virtual IEnumerable<T> Query<T, T2, T3, T4>(Sql sql)
        {
            return db.Query<T, T2, T3, T4>(sql);
        }

        public virtual IEnumerable<T> Query<T, T2, T3, T4, T5>(Sql sql)
        {
            return db.Query<T, T2, T3, T4, T5>(sql);
        }

        public virtual IEnumerable<TRet> Query<TRet>(Type[] types, object cb, string sql, params object[] args)
        {
            return db.Query<TRet>(types, cb, sql, args);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback)
        {
            await db.QueryAsync<T>(receivePocoCallback);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType)
        {
            await db.QueryAsync<T>(receivePocoCallback, commandType);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken, commandType);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, string sql, params object[] args)
        {
            await db.QueryAsync<T>(receivePocoCallback, sql, args);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType, string sql, params object[] args)
        {
            await db.QueryAsync<T>(receivePocoCallback, commandType, sql, args);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, string sql, params object[] args)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken, sql, args);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken, commandType, sql, args);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, Sql sql)
        {
            await db.QueryAsync<T>(receivePocoCallback, sql);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType, Sql sql)
        {
            await db.QueryAsync<T>(receivePocoCallback, commandType, sql);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, Sql sql)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken, sql);
        }

        public virtual async Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType, Sql sql)
        {
            await db.QueryAsync<T>(receivePocoCallback, cancellationToken, commandType, sql);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync()
        {
            return await db.QueryAsync<T>();
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CommandType commandType)
        {
            return await db.QueryAsync<T>(commandType);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken)
        {
            return await db.QueryAsync<T>(cancellationToken);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType)
        {
            return await db.QueryAsync<T>(cancellationToken, commandType);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(string sql, params object[] args)
        {
            return await db.QueryAsync<T>(sql, args);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CommandType commandType, string sql, params object[] args)
        {
            return await db.QueryAsync<T>(commandType, sql, args);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, string sql, params object[] args)
        {
            return await db.QueryAsync<T>(cancellationToken, sql, args);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args)
        {
            return await db.QueryAsync<T>(cancellationToken, commandType, sql, args);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(Sql sql)
        {
            return await db.QueryAsync<T>(sql);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CommandType commandType, Sql sql)
        {
            return await db.QueryAsync<T>(commandType, sql);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.QueryAsync<T>(cancellationToken, sql);
        }

        public virtual async Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType, Sql sql)
        {
            return await db.QueryAsync<T>(cancellationToken, commandType, sql);
        }

        public virtual IGridReader QueryMultiple(Sql sql)
        {
            return db.QueryMultiple(sql);
        }

        public virtual IGridReader QueryMultiple(string sql, params object[] args)
        {
            return db.QueryMultiple(sql, args);
        }

        public virtual void Save(T poco)
        {
            db.Save(poco);
        }

        public virtual async Task SaveAsync(T poco)
        {
            await db.SaveAsync(poco);
        }

        public virtual async Task SaveAsync(CancellationToken cancellationToken, T poco)
        {
            await SaveAsync(cancellationToken, poco);
        }
         

        public virtual List<T> SkipTake(long skip, long take)
        {
            return db.SkipTake<T>(skip, take);
        }

        public virtual List<T> SkipTake(long skip, long take, string sql, params object[] args)
        {
            return db.SkipTake<T>(skip, take, sql, args);
        }

        public virtual List<T> SkipTake(long skip, long take, Sql sql)
        {
            return db.SkipTake<T>(skip, take, sql);
        }

        public virtual async Task<List<T>> SkipTakeAsync(long skip, long take)
        {
            return await db.SkipTakeAsync<T>(skip, take);
        }

        public virtual async Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take)
        {
            return await db.SkipTakeAsync<T>(cancellationToken, skip, take);
        }

        public virtual async Task<List<T>> SkipTakeAsync(long skip, long take, string sql, params object[] args)
        {
            return await db.SkipTakeAsync<T>(skip, take, sql, args);
        }

        public virtual async Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take, string sql, params object[] args)
        {
            return await db.SkipTakeAsync<T>(cancellationToken, skip, take, sql, args);
        }

        public virtual async Task<List<T>> SkipTakeAsync(long skip, long take, Sql sql)
        {
            return await db.SkipTakeAsync<T>(skip, take, sql);
        }

        public virtual async Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take, Sql sql)
        {
            return await db.SkipTakeAsync<T>(cancellationToken, skip, take, sql);
        }

        public virtual int Update(T poco)
        {
            return db.Update(poco);
        }

        public virtual int Update(Sql sql)
        {
            return db.Update<T>(sql);
        }

        public virtual int Update(string sql, params object[] args)
        {
            return db.Update<T>(sql, args);
        }

        public virtual async Task<int> UpdateAsync(T poco)
        {
            return await db.UpdateAsync(poco);
        }

        public virtual async Task<int> UpdateAsync(string sql, params object[] args)
        {
            return await db.UpdateAsync<T>(sql, args);
        }

        public virtual async Task<int> UpdateAsync(Sql sql)
        {
            return await db.UpdateAsync<T>(sql);
        }

        public virtual async Task<int> UpdateAsync(Sql sql, params object[] args)
        {
            return await db.UpdateAsync<T>(sql.SQL,args);
        }

        public virtual async Task<int> UpdateAsync(CancellationToken cancellationToken, T poco)
        {
            return await db.UpdateAsync(cancellationToken,poco);
        }

        public virtual async Task<int> UpdateAsync(T poco, TPrimaryKey primaryKeyValue)
        {
            return await db.UpdateAsync(poco, primaryKeyValue);
        }

        public virtual async Task<int> UpdateAsync(CancellationToken cancellationToken, T poco, TPrimaryKey primaryKeyValue)
        {
            return await db.UpdateAsync(cancellationToken, poco, primaryKeyValue);
        }

        public virtual async Task<int> UpdateAsync(CancellationToken cancellationToken, Sql sql)
        {
            return await db.UpdateAsync<T>(cancellationToken, sql);
        }

         string GetPrimaryKey()
        {
            var pd = PocoData.ForType(typeof(T), db.DefaultMapper);
            if (string.IsNullOrWhiteSpace(pd.TableInfo.PrimaryKey))
                return "Id";
           
            return pd.TableInfo.PrimaryKey;
        }

        string GetPrimaryKeyValue()
        {
            var pkAttr = typeof(T).GetCustomAttributes(typeof(PrimaryKeyAttribute), true).FirstOrDefault() as PrimaryKeyAttribute;
            return pkAttr?.Value;
        }
    }
}
