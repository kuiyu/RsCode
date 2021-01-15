
using Microsoft.Extensions.DependencyInjection;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RsCode.Domain.Uow
{
    public static class UnitOfWorkServiceCollectionExtensions
    { 
        public static void AddUnitOfWork(this IServiceCollection services) 
        {
            services.TryAddTransient<IApplicationDbContext, ApplicationDbContext>();
            //services.TryAddTransient(typeof(IRepository<>), typeof(Repository<>));
         
            services.ConfigureDynamicProxy(config =>
            { 
              //  config.Interceptors.AddTyped<UnitOfWorkAttribute>(new AspectPredicate[] { Predicates.ForService("*Repository"), Predicates.ForService("*Service") });
            }); 
        }

    }
}
