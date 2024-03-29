﻿/*
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

namespace RsCode.DI
{
    /// <summary>
    ///  瞬时（Transient）生命周期服务在它们每次请求时被创建。
    ///  这一生命周期适合轻量级的，无状态的服务
    /// </summary>
    public interface ITransientDependency
    {
    }
}
