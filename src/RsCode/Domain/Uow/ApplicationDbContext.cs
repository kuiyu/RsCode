/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * MIT License
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

using FreeSql;

namespace RsCode.Domain.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : IApplicationDbContext
    {
       FreeSqlCloud<string> fsqlCloud;

        IFreeSql db;
		/// <summary>
		/// 数据库上下文
		/// </summary>
		/// <param name="databases"></param>
		/// <param name="configuration"></param>
		public ApplicationDbContext()
        {
            db = DbServiceCollectionExtensions.fsql; 
            fsqlCloud = DbServiceCollectionExtensions.fsql; 
        }


     
        /// <summary>
        /// 当前数据库连接
        /// </summary>
        public IFreeSql Current
        {
            get
            {
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
        public virtual IFreeSql ChangeDatabase(string connName = "DefaultConnection")
        {
           return fsqlCloud.Change(connName);
        }

        public IRepositoryUnitOfWork CreateUnitOfWork()
        {
            var uow = db.CreateUnitOfWork();
            CallContext<IRepositoryUnitOfWork>.SetData("rscode-uow", uow);
            return uow;
        }
      

        public IBaseRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var uow = CallContext<IRepositoryUnitOfWork>.GetData("rscode-uow");
            if(uow==null)
            {
                return db.GetRepository<TEntity>();
            }else
            {
                return uow.GetRepository<TEntity>();    
            }
            
        }

        public IBaseRepository<TEntity,TKey> GetRepository<TEntity, TKey>()
           where TEntity : class
        {
            var uow = CallContext<IRepositoryUnitOfWork>.GetData("rscode-uow");
            if (uow == null)
            {
                return db.GetRepository<TEntity,TKey>();
            }
            else
            {
                return uow.GetRepository<TEntity, TKey>();
            }
        }

    }
}
