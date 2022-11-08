using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;


using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RsCode.AspNetCore.ExceptionHandling
{
    public class ExceptionInterceptor : AbstractInterceptor
    { 
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
             
            return next(context);
        }
    }
}
