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
using System.Collections;
using System.Collections.Generic;
using Mapster;

namespace System
{
    /// <summary>
    /// 改用Mapster,
    /// 为了防止之前应用报错
    /// </summary>
    public static class AutoMapperExtensions
    {


        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="obj">可以使用这个扩展方法的类型</param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TDestination>(this object obj)
            where TDestination : class
        {
           return  obj.Adapt<TDestination>();
        }
        /// <summary>
        /// 复杂类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="obj">可以使用这个扩展方法的类型</param>
        /// <param name="configure"></param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TDestination>(this object obj, TypeAdapterConfig config)
            where TDestination : class
        {
            return obj.Adapt<TDestination>(config);
        }
        /// <summary>
        ///  类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <typeparam name="TSource">要被转化的model</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型</param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TDestination, TSource>(this TSource source)
            where TDestination : class
            where TSource : class
        {
            return source.Adapt<TDestination>();
        }

        /// <summary>
        ///  类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <typeparam name="TSource">要被转化的model</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型</param>
        /// <returns>转化之后的实体</returns>
        public static List<TDestination> MapTo<TDestination>(this IEnumerable source)
            where TDestination : class
        {
            var target = source.Adapt<List<TDestination>>();
            return target;
        }

        /// <summary>
        ///  类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <typeparam name="TSource">要被转化的model</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型</param>
        /// <returns>转化之后的实体</returns>
        public static List<TDestination> MapTo<TDestination, TSource>(this IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            return source.Adapt<List<TDestination>>();
        }

    }

 


}
