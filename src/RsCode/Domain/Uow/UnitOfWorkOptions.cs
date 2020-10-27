using System;
using System.Collections.Generic;
using System.Text;
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
