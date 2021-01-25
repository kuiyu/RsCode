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

namespace RsCode
{
    public interface IApplicationDbContext:RsCode.DI.ITransientDependency
    {
        IDatabase GetDatabase(string connName = "DefaultConnection");

        IDatabase Current { get; set; }
    }
}
