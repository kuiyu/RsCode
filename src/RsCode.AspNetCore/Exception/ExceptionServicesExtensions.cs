using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RsCode.Exceptions;
using System.Linq;

namespace RsCode.AspNetCore
{
    public static class ExceptionServicesExtensions
    {
        /// <summary>
        /// 添加模型验证错误处理 
        /// </summary>
        /// <param name="services"></param>
        public static void AddExceptionFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //设置返回内容
                    var result = new ReturnInfo
                    {
                        code = 500,
                        Success = false,
                        Msg = str
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
