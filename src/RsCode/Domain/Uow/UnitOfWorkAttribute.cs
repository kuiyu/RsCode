﻿using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using RsCode.AspNetCore;
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
        public UnitOfWorkAttribute(string ConnectionStringName)
        {
            this.DbConnectionStringName = ConnectionStringName; 
        }
        public override int Order { get; set; } = -10000;
        public bool Transaction { get; set; } = true;

        public string DbConnectionStringName { get; set; } = "DefaultConnection";
        
        
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {            
            var serviceType = context.ServiceMethod.DeclaringType;
             // context.Invoke(next);
            //if (serviceType.FullName.EndsWith("Service") || serviceType.FullName.EndsWith("Repository"))
            //{
                var dbContext = context.ServiceProvider.Resolve<IApplicationDbContext>();
                var db = dbContext.Current;
                var log = context.ServiceProvider.Resolve<ILogger<UnitOfWorkAttribute>>();
                try
                {
                    if (db.Connection == null)
                    {
                        await db.OpenSharedConnectionAsync();
                    }

                    await db.BeginTransactionAsync();
                    await context.Invoke(next);
                    db.CompleteTransaction();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException is AppException)
                        {
                            throw ex.InnerException as AppException;
                        }
                        else
                        {
                            if (log != null)
                                log.LogError($"{ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
                        }
                    }
                    else
                    {

                        if (ex is AppException)
                        {
                            throw ex as AppException;
                        }
                        else
                        {
                            if (log != null)
                                log.LogError($"{ex.Message}\n{ex.StackTrace}");
                        }
                    }
                   
                       db.AbortTransaction();
                    throw ex;
                }
                finally
                {
                    db.CloseSharedConnection();
                }

                //}
                //else
                //{
                //    await context.Invoke(next);
                //    return;
                //}
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
