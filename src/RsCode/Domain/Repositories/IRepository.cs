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

using RsCode.Domain.Aggregate;
using System.Threading.Tasks;

namespace RsCode.Domain.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> 
        where TEntity : Entity<TPrimaryKey>,  IAggregateRoot
    {
 
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : Entity<long>, IAggregateRoot
    {
        TEntity Get(long Id);
        Task<TEntity> GetAsync(long Id);

        bool Update(TEntity entity, long Id);
        Task<bool> UpdateAsync(TEntity entity, long Id);
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity); 

    }
}
