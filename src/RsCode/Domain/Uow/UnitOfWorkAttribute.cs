using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using RsCode.AspNetCore;
using System;
using System.Threading.Tasks;

namespace RsCode.Domain.Uow
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class UnitOfWorkAttribute : AbstractInterceptorAttribute
    { 
        public UnitOfWorkAttribute()
        {

        }
        public UnitOfWorkAttribute(string ConnectionStringName)
        {
            this.DbConnectionStringName = ConnectionStringName; 
        }
        public override int Order { get; set; } = -10000;
        public bool Transaction { get; set; } = true;

        public string DbConnectionStringName { get; set; } = "DefaultConnection";
        
        [FromServiceContext]
        public ILogger log { get; set; }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var serviceType = context.ServiceMethod.DeclaringType;
            if (serviceType.FullName.EndsWith("Service") || serviceType.FullName.EndsWith("Repository"))
            { 
                var dbContext = context.ServiceProvider.Resolve<IApplicationDbContext>();
                var db= dbContext.Current; 

                try
                {                  
                    using (db)
                    {
                        await db.OpenSharedConnectionAsync();
                        await db.BeginTransactionAsync();
                        await context.Invoke(next);
                        db.CompleteTransaction();
                    } 
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    string err = ex.Message;
                    if (ex is AppException)
                    {
                        err = ((AppException)ex).Message;
                    }
                    if (ex.InnerException!=null)
                    {
                        err = ex.InnerException.Message;
                        if(ex.InnerException is AppException)
                        {
                            err=((RsCode.AspNetCore.AppException)ex.InnerException).Message;
                        } 
                        
                    }
                    if(log!=null)
                    {
                        log.LogError(ex.StackTrace);
                    }

                    throw new AppException(err);
                }
                finally
                {
                    //if(db.Connection.State== System.Data.ConnectionState.Open)
                    //{
                    //    db.Connection.Close();
                    //    db.CloseSharedConnection(); 
                    //}
                   //db.Dispose();
                }
            }
            else
            {
                await context.Invoke(next);
                return;
            }
        }

        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions {
                Transaction = this.Transaction,
                DefaultConnection = this.DbConnectionStringName  
            };
        }

       
    }
}
