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
    /// 单例（Singleton）生命周期服务在它们第一次被解析时创建，并且每个后续解析将使用相同的实例。
    /// 如果你的应用程序需要单例行为，建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。
    /// </summary>
    public interface ISingletonDependency
    {
    }
}
