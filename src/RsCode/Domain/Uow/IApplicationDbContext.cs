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
using PetaPoco;
using System.Threading.Tasks;

namespace RsCode
{
    public interface IApplicationDbContext:RsCode.DI.ITransientDependency
    {
        IDatabase GetDatabase(string connName = "DefaultConnection");

        IDatabase Current { get; set; }
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">主键值</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(object primaryKeyValue);
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>返回新增记录主键值</returns>
        Task<object> InsertAsync<T>(T t);
        /// <summary>
        /// 保存记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task SaveAsync<T>(T t);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue"></param>
        /// <returns>返回删除记录条数</returns>
        Task<int> DeleteAsync<T>(object primaryKeyValue);
    }
}
