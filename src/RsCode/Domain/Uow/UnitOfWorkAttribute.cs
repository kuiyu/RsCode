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


using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using FreeSql;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RsCode
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class UnitOfWorkAttribute : AbstractInterceptorAttribute
    {

        [FromServiceContext]
        public IApplicationDbContext applicationDbContext { get; set; }

        string NewConnName = "DefaultConnection";


        public UnitOfWorkAttribute()
        {
            
        }
        public UnitOfWorkAttribute(string connName = "DefaultConnection")
        {
            NewConnName = connName;
            applicationDbContext =CallContext<IApplicationDbContext>.GetData("rswl-IApplicationDbContext");
            applicationDbContext.ChangeDatabase(connName);
            
        }
        public override int Order { get; set; } = -10000;


        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            using (var uow = applicationDbContext.CreateUnitOfWork())
            {
                try
                {
                    await next(context);
                    uow.Commit();
                }
                finally
                {
                    CallContext<IRepositoryUnitOfWork>.SetData("rscode-uow", null);

                    string connName = CallContext<string>.GetData("rswl-connName");
                    if (connName != NewConnName)
                    {
                        applicationDbContext.ChangeDatabase(connName);
                    }
                }
            }
        }
    }
}
