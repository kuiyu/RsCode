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
