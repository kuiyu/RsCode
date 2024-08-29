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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RsCode.AspNetCore.Plugin
{
    public interface IPluginSetup
    {
        /// <summary>
        /// 中间件顺序:
        /// 在 Startup 中添加中间件组件的顺序。
        /// Configure 方法定义了中间件组件在处理请求时被调用的顺序，以及在响应时的相反顺序。
        /// 这个顺序对于安全性、性能和功能至关重要。
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware
        /// </summary>
        int Order { get; }
       
        void ConfigureServices(IServiceCollection services);
        
        void Configure(IApplicationBuilder app, IHostEnvironment env);
    }
     
}
