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

using Microsoft.Extensions.DependencyInjection;

namespace RsCode
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 添加异常记录
        /// </summary>
        /// <param name="services"></param>
        public static void AddExceptionLogging(this IServiceCollection services)
        {
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<ExceptionInterceptor>();
            //});
            //var config = services.BuildServiceContextProvider().GetService<IConfiguration>();
            //var sql = CreateTableScript();
            //using (var connection = new MySqlConnection(config.GetConnectionString("DefaultConnection")))
            //    connection.ExecuteNonQuery(sql);
            
            
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddDelegate(async (context, next) =>
            //    {
            //        try
            //        {
            //            await context.Invoke(next);
            //        }
            //        catch (AppException e)
            //        {
            //            throw e;
            //        }
            //        catch (Exception ex)
            //        {
            //            LogHelper.WriteLog(context.Implementation.GetType(), ex);
            //            throw ex;
            //        }
            //    });
            //});
        }

        static string  CreateTableScript()
        {
            string sql =
                $@"create table if not exists rscode_system_log
                (
                    Id                   bigint not null auto_increment,
                    LogDate              datetime not null,
                    LogLevel             varchar(20) not null,
                    Logger               varchar(255) not null,
                    LogMessage           text not null,
                    ExceptionData        text not null,
                    primary key (Id)
                );";
            return sql;
        }
    }
}
