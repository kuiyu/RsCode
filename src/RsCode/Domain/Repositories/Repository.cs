﻿/*
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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RsCode.Domain
{

    public partial class  Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
         
        IApplicationDbContext applicationDbContext;
        
        public Repository( IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public PageData<TEntity> Page(int page, int pageSize)
        {
            var list = applicationDbContext.GetRepository<TEntity>().Select
                 .Count(out var total)
                 .Page(page, pageSize)
                 .ToList();
            PageData<TEntity> pageInfo = new PageData<TEntity>
            {
                Items = list,
                TotalPages = 0,
                CurrentPage = page,
                TotalItems = total
            };

            var totalPages = Convert.ToInt32(total / pageSize);
            if (total % pageSize > 0) totalPages++;
            pageInfo.TotalPages = totalPages;
            return pageInfo;
        }

        public PageData<TEntity> Page(int page, int pageSize, Expression<Func<TEntity, bool>> expression)
             
        {
            var list = applicationDbContext.GetRepository<TEntity>().Select
               .Count(out var total)
               .Page(page, pageSize)
               .Where(expression)
               .ToList();
            PageData<TEntity> pageInfo = new PageData<TEntity>
            {
                Items = list,
                TotalPages = 0,
                CurrentPage = page,
                TotalItems = total
            };

            var totalPages = Convert.ToInt32(total / pageSize);
            if (total % pageSize > 0) totalPages++;
            pageInfo.TotalPages = totalPages;
            return pageInfo;
        }
        public ISelect<TEntity> Select =>applicationDbContext.GetRepository<TEntity>().Select;

        

        public void Attach(TEntity entity)
        {
            applicationDbContext.GetRepository<TEntity>().Attach(entity);
        }

        public void Attach(IEnumerable<TEntity> entity)
        {
            applicationDbContext.GetRepository<TEntity>().Attach(entity);
        }

        public IBaseRepository<TEntity> AttachOnlyPrimary(TEntity data)
        {
            return applicationDbContext.GetRepository<TEntity>().AttachOnlyPrimary(data);
        }

        public void BeginEdit(List<TEntity> data)
        {
            applicationDbContext.GetRepository<TEntity>().BeginEdit(data); 
        }

        public Dictionary<string, object[]> CompareState(TEntity newdata)
        {
            return applicationDbContext.GetRepository<TEntity>().CompareState(newdata);
        }

        public int Delete(TEntity entity)
        {
            return applicationDbContext.GetRepository<TEntity>().Delete(entity);
        }

        public int Delete(IEnumerable<TEntity> entitys)
        {
            return applicationDbContext.GetRepository<TEntity>().Delete(entitys);
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return applicationDbContext.GetRepository<TEntity>().Delete(predicate);
        }

        public List<object> DeleteCascadeByDatabase(Expression<Func<TEntity, bool>> predicate)
        {
            return applicationDbContext.GetRepository<TEntity>().DeleteCascadeByDatabase(predicate);
        }

        public int EndEdit(List<TEntity> data = null)
        {
            return applicationDbContext.GetRepository<TEntity>().EndEdit(data);
        }

        public void FlushState()
        {
            applicationDbContext.GetRepository<TEntity>().FlushState();
        }

        public TEntity Insert(TEntity entity)
        {
            return applicationDbContext.GetRepository<TEntity>().Insert(entity);
        }

        public List<TEntity> Insert(IEnumerable<TEntity> entitys)
        {
            return applicationDbContext.GetRepository<TEntity>().Insert(entitys);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return applicationDbContext.GetRepository<TEntity>().InsertOrUpdate(entity);
        }

        public void SaveMany(TEntity entity, string propertyName)
        {
            applicationDbContext.GetRepository<TEntity>().SaveMany(entity, propertyName);
        }

        public int Update(TEntity entity)
        {
            return  applicationDbContext.GetRepository<TEntity>().Update(entity);
        }

        public int Update(IEnumerable<TEntity> entitys)
        {
            return applicationDbContext.GetRepository<TEntity>().Update(entitys);
        }

        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> exp)
        {
            return applicationDbContext.GetRepository<TEntity>().Where(exp);
        }

        public ISelect<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> exp)
        {
            return applicationDbContext.GetRepository<TEntity>().WhereIf(condition, exp);
        }
    }
    public partial class Repository<TEntity, TKey> :Repository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class
    {
        IApplicationDbContext applicationDbContext;
        IFreeSql db;
        
     
        public Repository(IApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
            db = applicationDbContext.Current;
        }

     
        public TEntity Get(TKey id)
        {
            return applicationDbContext.GetRepository<TEntity, TKey>().Get(id);
        }

        public TEntity Find(TKey id)
        {
            return applicationDbContext.GetRepository<TEntity, TKey>().Find(id);
        }

        public int Delete(TKey id)
        {
            return applicationDbContext.GetRepository<TEntity, TKey>().Delete(id);
        }

        public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return applicationDbContext.GetRepository<TEntity, TKey>().GetAsync(id, cancellationToken);
        }

        public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return applicationDbContext.GetRepository<TEntity, TKey>().FindAsync(id, cancellationToken);
        }

        public Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
           return applicationDbContext.GetRepository<TEntity,TKey>().DeleteAsync(id, cancellationToken);    
        }

     
    }

 
}
