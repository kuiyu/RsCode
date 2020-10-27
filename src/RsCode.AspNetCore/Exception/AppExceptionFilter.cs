using AspectCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// 业务级异常过滤器
    /// </summary>
    public class AppExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        [FromServiceContext]
        public ILogger log{ get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception!=null)
            {
                if(context.Exception.InnerException!=null)
                {
                    if (context.Exception.InnerException is AppException)
                    {
                        var appException = context.Exception.InnerException as AppException;
                        var msg = ((AppException)context.Exception.InnerException).Message;
                        ObjectResult objectResult = new ObjectResult(appException.Message)
                        {
                            StatusCode = appException.Status
                        };
                        objectResult.DeclaredType = typeof(AppException);
                        context.Result = objectResult;

                        context.ExceptionHandled = true;
                    }
                }
                else
                {
                    if (context.Exception is AppException exception)
                    {
                        ObjectResult objectResult = new ObjectResult(exception.Message)
                        {
                            StatusCode = exception.Status
                        };
                        objectResult.DeclaredType = typeof(AppException);
                        context.Result = objectResult;

                        context.ExceptionHandled = true;
                    }
                }
            }
               
            if(log!=null)
            {
                log.LogError(context.Exception.StackTrace);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
