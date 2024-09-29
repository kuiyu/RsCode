/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
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
		public ApplicationDbContext()
        {
            db = DbServiceCollectionExtensions.fsql; 
            fsqlCloud = DbServiceCollectionExtensions.fsql;
            CallContext<string>.SetData("rswl-connName", "DefaultConnection");
            CallContext<IApplicationDbContext>.SetData("rswl-IApplicationDbContext",this);
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
        /// 把数据库更换为指定数据库连接字符串key的数据库配置
        /// </summary>
        /// <param name="connName">默认值为DefaultConnection</param>
        /// <param name="forever">是否永久更换</param>
        /// <returns></returns>
        public virtual IFreeSql ChangeDatabase(string connName = "DefaultConnection",bool forever=false)
        {
            if(forever)
            {
                CallContext<string>.SetData("rswl-connName", connName);
            }
           return fsqlCloud.Change(connName);
        }
        /// <summary>
        /// 临时变更数据库
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        public virtual IFreeSql UseDatabase(string connName="DefaultConnection")
        {
            return fsqlCloud.Use(connName);
        }
        /// <summary>
        /// 创建UnitOfWork
        /// </summary>
        /// <returns></returns>
        public IRepositoryUnitOfWork CreateUnitOfWork()
        {
            var uow = db.CreateUnitOfWork();
            CallContext<IRepositoryUnitOfWork>.SetData("rscode-uow", uow);
            return uow;
        }
      
        /// <summary>
        /// 获取仓储服务实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
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
