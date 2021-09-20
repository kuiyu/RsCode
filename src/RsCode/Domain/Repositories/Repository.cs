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
using PetaPoco.Core;
using RsCode.Domain.Aggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsCode.Domain.Repositories
{

    public abstract class Repository<TEntity>:Repository<TEntity,long>,IRepository<TEntity,long> 
        where TEntity:Entity<long>,IAggregateRoot
    {
        
    }

    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
       where TEntity : Entity<TPrimaryKey>, IAggregateRoot
    {
        IApplicationDbContext dbContext;
        public IDatabase db { get;private set; }
        public virtual async Task<TPrimaryKey> InsertAsync(TEntity poco)
        {
            var obj = await db.InsertAsync(poco);
            if (!Convert.IsDBNull(obj))
            { 
                return (TPrimaryKey)obj;
            }
            return default(TPrimaryKey);

           
        }

        public virtual async Task SaveAsync<TEntity>(TEntity entity)
        {
            
        }

        string GetPrimaryKey()
        {
            var pd = PocoData.ForType(typeof(Entity), db.DefaultMapper);
            return pd.TableInfo.PrimaryKey;
        }

        string GetPrimaryKeyValue()
        {
            var pkAttr = typeof(TEntity).GetCustomAttributes(typeof(PrimaryKeyAttribute), true).FirstOrDefault() as PrimaryKeyAttribute;
            return pkAttr?.Value;
        }
        
        
    }


     
}
