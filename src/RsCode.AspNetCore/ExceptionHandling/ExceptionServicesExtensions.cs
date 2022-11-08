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
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace RsCode.AspNetCore
{
    public static class ExceptionServicesExtensions
    {
        /// <summary>
        /// 添加模型验证错误处理 
        /// </summary>
        /// <param name="services"></param>
        public static void AddErrorHandler(this IServiceCollection services)
        {
            
            
            services.AddMvc(options =>
            {
               // options.Filters.Add<AppExceptionFilter>();
                //统一输入内容格式
                options.InputFormatters.Insert(0, new RsInputFormatter());
                //统一的消息返回格式
                options.OutputFormatters.Insert(0, new RsOutputFormatter("yyyy-MM-dd HH:mm:ss"));
            });
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
