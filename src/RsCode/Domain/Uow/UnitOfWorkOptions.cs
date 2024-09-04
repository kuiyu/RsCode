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
using System.Transactions;

namespace RsCode.Domain.Uow
{
    public class UnitOfWorkOptions
    {
        public TransactionScopeOption? Scope { get; set; }
        public bool Transaction { get; set; }
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public string DefaultConnection { get; set; } = "DefaultConnection";
    }
}
