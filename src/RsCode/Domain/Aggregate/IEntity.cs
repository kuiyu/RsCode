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
 */

namespace RsCode.Domain
{
    /// <summary>
    /// 实体类
    /// </summary>
    public interface IEntity :IEntity<long>
    {
      
    }

    public interface IEntity<TPrimaryKey>
    { 
    }
 
}
