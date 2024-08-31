﻿/*
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
using FreeSql;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace RsCode.Domain
{

    public interface IRepository<TEntity>  where TEntity : class
    {
        PageData<TEntity> Page(int page, int pageSize);
        PageData<TEntity> Page(int page, int pageSize, Expression<Func<TEntity, bool>> expression);
        ISelect<TEntity> Select { get; }

        ISelect<TEntity> Where(Expression<Func<TEntity, bool>> exp);
        ISelect<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> exp);

        TEntity Insert(TEntity entity);
        List<TEntity> Insert(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 清空状态数据
        /// </summary>
        void FlushState();
        /// <summary>
        /// 附加实体，可用于不查询就更新或删除
        /// </summary>
        /// <param name="entity"></param>
        void Attach(TEntity entity);
        void Attach(IEnumerable<TEntity> entity);
        /// <summary>
        /// 附加实体，并且只附加主键值，可用于不更新属性值为null或默认值的字段
        /// </summary>
        /// <param name="data"></param>
        IBaseRepository<TEntity> AttachOnlyPrimary(TEntity data);
        /// <summary>
        /// 比较实体，计算出值发生变化的属性，以及属性变化的前后值
        /// </summary>
        /// <param name="newdata">最新的实体对象，它将与附加实体的状态对比</param>
        /// <returns>key: 属性名, value: [旧值, 新值]</returns>
        Dictionary<string, object[]> CompareState(TEntity newdata);

        int Update(TEntity entity);
        int Update(IEnumerable<TEntity> entitys);

        TEntity InsertOrUpdate(TEntity entity);
        /// <summary>
        /// 保存实体的指定 ManyToMany/OneToMany 导航属性（完整对比）<para></para>
        /// 场景：在关闭级联保存功能之后，手工使用本方法<para></para>
        /// 例子：保存商品的 OneToMany 集合属性，SaveMany(goods, "Skus")<para></para>
        /// 当 goods.Skus 为空(非null)时，会删除表中已存在的所有数据<para></para>
        /// 当 goods.Skus 不为空(非null)时，添加/更新后，删除表中不存在 Skus 集合属性的所有记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名</param>
        void SaveMany(TEntity entity, string propertyName);

      

        int Delete(TEntity entity);
        int Delete(IEnumerable<TEntity> entitys);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据设置的 OneToOne/OneToMany/ManyToMany 导航属性，级联查询所有的数据库记录，删除并返回它们
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<object> DeleteCascadeByDatabase(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 开始编辑数据，然后调用方法 EndEdit 分析出添加、修改、删除 SQL 语句进行执行<para></para>
        /// 场景：winform 加载表数据后，一顿添加、修改、删除操作之后，最后才点击【保存】<para></para><para></para>
        /// 示例：https://github.com/dotnetcore/FreeSql/issues/397<para></para>
        /// 注意：* 本方法只支持单表操作，不支持导航属性级联保存
        /// </summary>
        /// <param name="data"></param>
        void BeginEdit(List<TEntity> data);
        /// <summary>
        /// 完成编辑数据，进行保存动作<para></para>
        /// 该方法根据 BeginEdit 传入的数据状态分析出添加、修改、删除 SQL 语句<para></para>
        /// 注意：* 本方法只支持单表操作，不支持导航属性级联保存
        /// </summary>
        /// <param name="data">可选参数：手工传递最终的 data 值进行对比<para></para>默认：如果不传递，则使用 BeginEdit 传入的 data 引用进行对比</param>
        /// <returns></returns>
        int EndEdit(List<TEntity> data = null);
    }
    public interface IRepository<TEntity, TKey>:IRepository<TEntity>
        where TEntity : class
    {
        TEntity Get(TKey id);
        TEntity Find(TKey id);
        int Delete(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
     
    }
  
}