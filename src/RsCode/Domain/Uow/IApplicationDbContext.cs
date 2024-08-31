/*
 * 项目：.net开发基础工具类
 * 作者：河南软商网络科技有限公司
 * * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using FreeSql;
using System.Linq.Expressions;
using System;

namespace RsCode
{
    public interface IApplicationDbContext:RsCode.DI.ISingletonDependency
    {
       
        IFreeSql Current { get; }

        IFreeSql ChangeDatabase(string connName = "DefaultConnection");

        IRepositoryUnitOfWork CreateUnitOfWork();

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity:class;
        IBaseRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : class;
    }
}
