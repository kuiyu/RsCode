using Microsoft.AspNetCore.Mvc.Filters;

namespace RsCode.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreOutFormatterAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var httpContextAccessor=serviceProvider.GetService<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.Items.Add("IgnoreOutFormatter", true);

            return new IgnoreOutFormatterFilter();
        }
    }
}
