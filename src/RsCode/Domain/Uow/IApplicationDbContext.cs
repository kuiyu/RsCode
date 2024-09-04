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

namespace RsCode
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// 当前数据库连接
        /// </summary>
        IFreeSql Current { get; }

        /// <summary>
        /// 把数据库更换为指定数据库连接字符串key的数据库配置
        /// </summary>
        /// <param name="connName">默认值为DefaultConnection</param>
        /// <param name="forever">是否永久更换</param>
        /// <returns></returns>
        IFreeSql ChangeDatabase(string connName = "DefaultConnection", bool forever = false);
        /// <summary>
        /// 创建UnitOfWork
        /// </summary>
        /// <returns></returns>
        IRepositoryUnitOfWork CreateUnitOfWork();
        /// <summary>
        /// 获取仓储服务实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity:class;
        /// <summary>
        /// 获取仓储服务实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IBaseRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : class;
    }
}
