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

 * 文档 https://rscode.cn/
 */

using PetaPoco;
using System;

namespace RsCode.Domain.Uow
{
    /// <summary>
    /// 数据操作：工作单元 
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
        IDatabase Open(string connName= "DefaultConnection");

      
    }
}
