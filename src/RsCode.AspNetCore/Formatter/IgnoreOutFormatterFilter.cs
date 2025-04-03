using Microsoft.AspNetCore.Mvc.Filters;

namespace RsCode.AspNetCore
{
    public class IgnoreOutFormatterFilter : IActionFilter, IResultFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
         
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.Add("IgnoreOutFormatter", "true");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
           
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
             
        }
    }
}
