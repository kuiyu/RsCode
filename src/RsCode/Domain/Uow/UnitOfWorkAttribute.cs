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


using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using RsCode.Domain.Uow;
using System;
using System.Threading.Tasks;

namespace RsCode
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class UnitOfWorkAttribute : AbstractInterceptorAttribute
    {
        public UnitOfWorkAttribute()
        {
            
        }
        [FromServiceContext]
        public IUnitOfWork Uow { get; set; }

        public UnitOfWorkAttribute(string connName)
        {
            this.DbConnectionStringName = connName; 
        }
        public override int Order { get; set; } = -10000;

        public string DbConnectionStringName { get; set; } = "DefaultConnection";

       
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                using (Uow)
                {
                    Uow.Open(DbConnectionStringName);
                    await next(context);
                    Uow.Commit();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                Uow.Dispose();
            }
                
           
        }


    }
}
