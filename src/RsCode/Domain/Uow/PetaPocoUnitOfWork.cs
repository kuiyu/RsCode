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

 * 文档 https://rscode.cn/
 */

using PetaPoco;

namespace RsCode.Domain.Uow
{
    /// <summary>
    /// PetaPoco UnitOfWork
    /// </summary>
    public class PetaPocoUnitOfWork:IUnitOfWork
    {
        private  Transaction _transaction;
        private  IDatabase _db;

        IApplicationDbContext applicationDbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public PetaPocoUnitOfWork(IApplicationDbContext dbContext)
        {
            applicationDbContext = dbContext;
            _db = dbContext.Current;
            _transaction = _db.Transaction as PetaPoco.Transaction;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _transaction?.Complete();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        public IDatabase Open(string connName = "DefaultConnection")
        {
            if(connName!= "DefaultConnection")
            {
               _db?.Dispose();
                _transaction?.Dispose();
               _db = applicationDbContext.GetDatabase(connName);
                _transaction = _db.Transaction as PetaPoco.Transaction;
            }
            
            return _db;
        }
    }
}
