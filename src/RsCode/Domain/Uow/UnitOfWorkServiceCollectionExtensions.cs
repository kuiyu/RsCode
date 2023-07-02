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


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RsCode.Domain.Uow;
using RsCode.Domain;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Configuration;

namespace RsCode
{
    public static class UnitOfWorkServiceCollectionExtensions
    { 
        public static void AddUnitOfWork(this IServiceCollection services) 
        {
            services.TryAddTransient<IApplicationDbContext, ApplicationDbContext>();

            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.TryAddScoped<IUnitOfWork, PetaPocoUnitOfWork>();

          
            services.ConfigureDynamicProxy(config =>
            {
                AspectPredicate[] aspectPredicates = new AspectPredicate[]
                {
                    Predicates.ForService("*Service"),
                    Predicates.ForService("*Repository")
                };
                config.Interceptors.AddTyped<UnitOfWorkAttribute>(aspectPredicates) ;
            });

        }

    }
}
