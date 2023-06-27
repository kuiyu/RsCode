//using PetaPoco;
//using RsCode.Domain.Uow;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace RsCode.Domain.Repositories
//{
//    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
//    {
//        private readonly string _connectionStringName;
//        private Database _db;
//        private IUnitOfWork _unitOfWork;

//        public Database DbContext
//        {
//            get
//            {
//                if (this._db == null)
//                {
//                    return new Database(_connectionStringName);

//                    //if (this._connectionStringName == string.Empty)
//                    //    return ((PetaPocoContext)DbContextManager.Current).Db;
//                    //else
//                    //    return ((PetaPocoContext)DbContextManager.CurrentFor(this._connectionStringName)).Db;
//                }
//                return this._db;
//            }
//        }

//        public GenericRepository() : this("Default")
//        {
//        }

//        public GenericRepository(string connectionStringName)
//        {
//            this._connectionStringName = connectionStringName;
//        }

//        public GenericRepository(PetaPocoContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException("context");

//            _db = context.Db;
//        }

//        public TEntity GetByKey(object keyValue)
//        {
//            return DbContext.SingleOrDefault<TEntity>(keyValue);
//        }

//        public IEnumerable<TEntity> GetAll()
//        {
//            var pd = TableInfo.FromPoco(typeof(TEntity));
//            var sql = "SELECT * FROM " + pd.TableName;

//            return DbContext.Query<TEntity>(sql);
//        }

//        public IEnumerable<TEntity> Find(string sqlCondition, params object[] args)
//        {
//            return DbContext.Query<TEntity>(sqlCondition, args);
//        }

//        public IEnumerable<TEntity> Query(Sql sql)
//        {
//            return DbContext.Query<TEntity>(sql);
//        }

//        public IEnumerable<TEntity> Find(Sql sql)
//        {
//            return DbContext.Fetch<TEntity>(sql);
//        }

//        public TEntity Single(string sqlCondition, params object[] args)
//        {
//            return DbContext.Single<TEntity>(sqlCondition, args);
//        }

//        public TEntity First(string sqlCondition, params object[] args)
//        {
//            return DbContext.First<TEntity>(sqlCondition, args);
//        }

//        public TEntity Insert(TEntity entity)
//        {
//            DbContext.Insert(entity);
//            return entity;
//        }

//        public virtual void Delete(TEntity entity)
//        {
//            DbContext.Delete(entity);
//        }

//        public virtual void Delete(string sqlCondition, params object[] args)
//        {
//            DbContext.Delete<TEntity>(sqlCondition, args);
//        }

//        public void Update(TEntity entity)
//        {
//            DbContext.Update(entity);
//        }

//        public void Save(TEntity entity)
//        {
//            DbContext.Save(entity);
//        }

//        public Page<TEntity> Get(long pageNumber, long itemsPerPage, string sqlCondition, params object[] args)
//        {
//            return DbContext.Page<TEntity>(pageNumber, itemsPerPage, sqlCondition, args);
//        }

//        public int Count()
//        {
//            return DbContext.ExecuteScalar<int>("select count(*) from " + TableInfo.FromPoco(typeof(TEntity)).TableName);
//        }

//        public int Count(string sqlCondition, params object[] args)
//        {
//            return DbContext.Query<TEntity>(sqlCondition, args).Count();
//        }

//        public IUnitOfWork UnitOfWork
//        {
//            get
//            {
//                if (_unitOfWork == null)
//                {
//                    _unitOfWork = new PetaPocoUnitOfWork(this.DbContext);
//                }
//                return _unitOfWork;
//            }
//        }
//    }
//}
