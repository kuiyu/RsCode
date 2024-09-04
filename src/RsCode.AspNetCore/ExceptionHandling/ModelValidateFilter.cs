/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public class ModelValidateFilter : IAsyncActionFilter
    {  
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                string errMsg = "";

                var errorResults = new List<ErrorResult>();
                foreach (var item in context.ModelState)
                {
                    var result = new ErrorResult()
                    {
                        Field = item.Key,
                    };
                    foreach (var error in item.Value.Errors)
                    {
                        if (!string.IsNullOrEmpty(result.Message))
                        {
                            result.Message += "|";
                        }

                        result.Message += error.ErrorMessage;
                        if(string.IsNullOrWhiteSpace(errMsg))
                        {
                            errMsg = error.ErrorMessage;
                        }
                    }
                    errorResults.Add(result);
                }


                var errInfo = new RsCode.ReturnInfo
                {
                    code = 200,
                    Result = errorResults,
                    Msg =errMsg,
                    Success = false
                };
                //context.Result = new BadRequestObjectResult(errInfo);
                context.Result = new ContentResult { 
                  StatusCode= 200,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonSerializer.Serialize(errInfo)
                };

            }else
            {
                await next();
            }
            
        }
    }

    internal class ErrorResult
    {
        public string Field { get; set; }

        public string Message { get; set; }
    }
}
