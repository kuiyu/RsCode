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
using PetaPoco;

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Domain
{
    public interface IRepository<T>:IRepository<T,long> where T : IEntity<long>
    {

    }
    public interface IRepository<T,TPrimaryKey> where T : IEntity<TPrimaryKey>
    {
        IDatabase ChangeDataBase(string connStrName);
         object ExecuteScalar(Sql sql);
        object ExecuteScalar(string sql, params object[] args);
        Task<object> ExecuteScalarAsync(CancellationToken cancellationToken, Sql sql);
        Task<object> ExecuteScalarAsync(CancellationToken cancellationToken, string sql, params object[] args);
        Task<object> ExecuteScalarAsync(Sql sql);
        Task<object> ExecuteScalarAsync(string sql, params object[] args);
         
        bool Exists(string sqlCondition, params object[] args);
        
        Task<bool> ExistsAsync(CancellationToken cancellationToken, string sqlCondition, params object[] args);
         
        Task<bool> ExistsAsync(string sqlCondition, params object[] args);
        List<T> Fetch();
        List<T> Fetch(long page, long itemsPerPage);
        List<T> Fetch(long page, long itemsPerPage, Sql sql);
        List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args);
        List<T> Fetch(Sql sql);
        List<T> Fetch(string sql, params object[] args);
        List<TRet> Fetch<T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, Sql sql);
        List<TRet> Fetch<T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, string sql, params object[] args);
        List<T> Fetch<T2, T3, T4, T5>(Sql sql);
        List<T> Fetch<T2, T3, T4, T5>(string sql, params object[] args);
        List<TRet> Fetch<T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, Sql sql);
        List<TRet> Fetch<T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, string sql, params object[] args);
        List<T> Fetch<T2, T3, T4>(Sql sql);
        List<T> Fetch<T2, T3, T4>(string sql, params object[] args);
        List<TRet> Fetch<T2, T3, TRet>(Func<T, T2, T3, TRet> cb, Sql sql);
        List<TRet> Fetch<T2, T3, TRet>(Func<T, T2, T3, TRet> cb, string sql, params object[] args);
        List<T> Fetch<T2, T3>(Sql sql);
        List<T> Fetch<T2, T3>(string sql, params object[] args);
        List<TRet> Fetch<T2, TRet>(Func<T, T2, TRet> cb, Sql sql);
        List<TRet> Fetch<T2, TRet>(Func<T, T2, TRet> cb, string sql, params object[] args);
        List<T> Fetch<T2>(Sql sql);
        List<T> Fetch<T2>(string sql, params object[] args);
        Task<List<T>> FetchAsync();
        Task<List<T>> FetchAsync(CancellationToken cancellationToken);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType, Sql sql);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sql);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sql, params object[] args);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, Sql sql);
        Task<List<T>> FetchAsync(CancellationToken cancellationToken, string sql, params object[] args);
        Task<List<T>> FetchAsync(CommandType commandType);
        Task<List<T>> FetchAsync(CommandType commandType, Sql sql);
        Task<List<T>> FetchAsync(CommandType commandType, string sql, params object[] args);
        Task<List<T>> FetchAsync(long page, long itemsPerPage);
        Task<List<T>> FetchAsync(long page, long itemsPerPage, Sql sql);
        Task<List<T>> FetchAsync(long page, long itemsPerPage, string sql, params object[] args);
        Task<List<T>> FetchAsync(Sql sql);
        Task<List<T>> FetchAsync(string sql, params object[] args);
        T First(Sql sql);
        T First(string sql, params object[] args);
        Task<T> FirstAsync(CancellationToken cancellationToken, Sql sql);
        Task<T> FirstAsync(CancellationToken cancellationToken, string sql, params object[] args);
        Task<T> FirstAsync(Sql sql);
        Task<T> FirstAsync(string sql, params object[] args);
        T FirstOrDefault(Sql sql);
        T FirstOrDefault(string sql, params object[] args);
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken, Sql sql);
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken, string sql, params object[] args);
        Task<T> FirstOrDefaultAsync(Sql sql);
        Task<T> FirstOrDefaultAsync(TPrimaryKey primaryKey);
        Task<T> FirstOrDefaultAsync(string sql, params object[] args);
        Page<T> Page(long page, long itemsPerPage);
        Page<T> Page(long page, long itemsPerPage, Sql sql);
        Page<T> Page(long page, long itemsPerPage, Sql sqlCount, Sql sqlPage);
        Page<T> Page(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs);
        Page<T> Page(long page, long itemsPerPage, string sql, params object[] args);
        Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage);
        Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sql);
        Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, Sql sqlCount, Sql sqlPage);
        Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs);
        Task<Page<T>> PageAsync(CancellationToken cancellationToken, long page, long itemsPerPage, string sql, params object[] args);
        Task<Page<T>> PageAsync(long page, long itemsPerPage);
        Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sql);
        Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sqlCount, Sql sqlPage);
        Task<Page<T>> PageAsync(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs);
        Task<Page<T>> PageAsync(long page, long itemsPerPage, string sql, params object[] args);
        IEnumerable<T> Query();
        IEnumerable<T> Query(Sql sql);
        IEnumerable<T> Query(string sql, params object[] args);
        IEnumerable<TRet> Query<T, T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, Sql sql);
        IEnumerable<TRet> Query<T, T2, T3, T4, T5, TRet>(Func<T, T2, T3, T4, T5, TRet> cb, string sql, params object[] args);
        IEnumerable<T> Query<T, T2, T3, T4, T5>(Sql sql);
        IEnumerable<T> Query<T, T2, T3, T4, T5>(string sql, params object[] args);
        IEnumerable<TRet> Query<T, T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, Sql sql);
        IEnumerable<TRet> Query<T, T2, T3, T4, TRet>(Func<T, T2, T3, T4, TRet> cb, string sql, params object[] args);
        IEnumerable<T> Query<T, T2, T3, T4>(Sql sql);
        IEnumerable<T> Query<T, T2, T3, T4>(string sql, params object[] args);
        IEnumerable<TRet> Query<T, T2, T3, TRet>(Func<T, T2, T3, TRet> cb, Sql sql);
        IEnumerable<TRet> Query<T, T2, T3, TRet>(Func<T, T2, T3, TRet> cb, string sql, params object[] args);
        IEnumerable<T> Query<T, T2, T3>(Sql sql);
        IEnumerable<T> Query<T, T2, T3>(string sql, params object[] args);
        IEnumerable<TRet> Query<T, T2, TRet>(Func<T, T2, TRet> cb, Sql sql);
        IEnumerable<TRet> Query<T, T2, TRet>(Func<T, T2, TRet> cb, string sql, params object[] args);
        IEnumerable<T> Query<T, T2>(Sql sql);
        IEnumerable<T> Query<T, T2>(string sql, params object[] args);
        IEnumerable<TRet> Query<TRet>(Type[] types, object cb, string sql, params object[] args);
        Task<IAsyncReader<T>> QueryAsync();
        Task QueryAsync(Action<T> receivePocoCallback);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType, Sql sql);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, Sql sql);
        Task QueryAsync(Action<T> receivePocoCallback, CancellationToken cancellationToken, string sql, params object[] args);
        Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType);
        Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType, Sql sql);
        Task QueryAsync(Action<T> receivePocoCallback, CommandType commandType, string sql, params object[] args);
        Task QueryAsync(Action<T> receivePocoCallback, Sql sql);
        Task QueryAsync(Action<T> receivePocoCallback, string sql, params object[] args);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType, Sql sql);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, CommandType commandType, string sql, params object[] args);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, Sql sql);
        Task<IAsyncReader<T>> QueryAsync(CancellationToken cancellationToken, string sql, params object[] args);
        Task<IAsyncReader<T>> QueryAsync(CommandType commandType);
        Task<IAsyncReader<T>> QueryAsync(CommandType commandType, Sql sql);
        Task<IAsyncReader<T>> QueryAsync(CommandType commandType, string sql, params object[] args);
        Task<IAsyncReader<T>> QueryAsync(Sql sql);
        Task<IAsyncReader<T>> QueryAsync(string sql, params object[] args);
        IGridReader QueryMultiple(Sql sql);
        IGridReader QueryMultiple(string sql, params object[] args);
       
        List<T> SkipTake(long skip, long take);
        List<T> SkipTake(long skip, long take, Sql sql);
        List<T> SkipTake(long skip, long take, string sql, params object[] args);
        Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take);
        Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take, Sql sql);
        Task<List<T>> SkipTakeAsync(CancellationToken cancellationToken, long skip, long take, string sql, params object[] args);
        Task<List<T>> SkipTakeAsync(long skip, long take);
        Task<List<T>> SkipTakeAsync(long skip, long take, Sql sql);
        Task<List<T>> SkipTakeAsync(long skip, long take, string sql, params object[] args);

        #region AlterPoco 
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="poco"></param>
        /// <returns>自动分配新记录的主键，对于非自动递增表为空。</returns>
        TPrimaryKey Insert(T poco);

        Task<TPrimaryKey> InsertAsync(T poco);

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="poco">自动分配新记录的主键，对于非自动递增表为空。</param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertAsync(CancellationToken cancellationToken, T poco);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="poco"></param>
        /// <returns>受影响的行数</returns>
        int Update(T poco);
        int Update(Sql sql);
        int Update(string sql, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="poco"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync(T poco);  
        Task<int>UpdateAsync(string sql, params object[] args); 
        Task<int> UpdateAsync(Sql sql);
        Task<int> UpdateAsync(Sql sql,params object[]args); 
      

        Task<int> UpdateAsync(CancellationToken cancellationToken, T poco);


        Task<int> UpdateAsync(T poco, TPrimaryKey primaryKeyValue);


        Task<int> UpdateAsync(CancellationToken cancellationToken, T poco, TPrimaryKey primaryKeyValue);

       
        Task<int> UpdateAsync(CancellationToken cancellationToken, Sql sql);

        /// <summary>
        ///     Performs an SQL Delete
        /// </summary>
        /// <param name="poco">The POCO object specifying the table name and primary key value of the row to be deleted</param>
        /// <returns>受影响的行数</returns>
        int Delete(T poco);

        int Delete(TPrimaryKey primaryKey);
        int Delete(string sql, params object[] args);
         
        int Delete(Sql sql); 
        Task<int> DeleteAsync(T poco);

        Task<int> DeleteAsync(TPrimaryKey primaryKey);
        Task<int> DeleteAsync(CancellationToken cancellationToken, T poco);
         
        Task<int> DeleteAsync(string sql, params object[] args);
 
        Task<int> DeleteAsync(CancellationToken cancellationToken, string sql, params object[] args);
       
        Task<int> DeleteAsync(Sql sql);

        Task<int> DeleteAsync(CancellationToken cancellationToken, Sql sql);


        
        void Save(T poco); 
        Task SaveAsync(T poco); 
       
        Task SaveAsync(CancellationToken cancellationToken, T poco);
        #endregion
    }
}