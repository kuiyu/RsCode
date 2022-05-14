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
using AutoMapper;
using AutoMapper.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace System
{
    /// <summary>
    /// AutoMapper扩展
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// 创建Mapper
        /// </summary>
        /// <typeparam name="TSource">要被转化的model</typeparam>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <returns></returns>
        private static IMapper CreateMapper<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.CreateMap<TSource, TDestination>();
            }); ;
            var mapper = config.CreateMapper();
            return mapper;
        }

        /// <summary>
        /// 创建Mapper
        /// </summary>
        /// <param name="sourceType">要被转化的model类型</param>
        /// <param name="destinationType">转化之后的model类型</param>
        /// <returns></returns>
        private static IMapper CreateMapper(Type sourceType, Type destinationType)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(sourceType, destinationType)); ;
            var mapper = config.CreateMapper();
            return mapper;
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="obj">可以使用这个扩展方法的类型</param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TDestination>(this object obj)
            where TDestination : class
        {
            var mapper = CreateMapper(obj.GetType(), typeof(TDestination));
            return mapper.Map<TDestination>(obj);
        }
        /// <summary>
        /// 复杂类型映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="obj">可以使用这个扩展方法的类型</param>
        /// <param name="configure"></param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TDestination>(this object obj,Action<IMapperConfigurationExpression>configure)
            where TDestination : class
        {
            var config=new MapperConfiguration(configure);
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(obj);
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
            if (source == null) return default(TDestination);
            var mapper = CreateMapper<TSource, TDestination>();
            return mapper.Map<TDestination>(source);
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
            if (source == null) return default(List<TDestination>);
            var mapper = CreateMapper(source.AsQueryable().ElementType, typeof(TDestination));
            return mapper.Map<List<TDestination>>(source);
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
            if (source == null) return default(List<TDestination>);
            var mapper = CreateMapper<TSource, TDestination>();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// DataTable映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="dt">DataTable对象</param>
        /// <returns></returns>
        public static List<TDestination> MapTo<TDestination>(this DataTable dt)
            where TDestination : class
        {
            if (dt == null || dt.Rows.Count == 0) return default(List<TDestination>);
            var mapper = CreateMapper<IDataReader, TDestination>();
            return mapper.Map<List<TDestination>>(dt.CreateDataReader());
        }

        /// <summary>
        /// DataSet映射
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model</typeparam>
        /// <param name="ds">DataSet对象</param>
        /// <param name="tableIndex">DataSet中要映射的DataTable的索引</param>
        /// <returns></returns>
        public static List<TDestination> MapTo<TDestination>(this DataSet ds, int tableIndex = 0)
           where TDestination : class
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return default(List<TDestination>);
            var mapper = CreateMapper<IDataReader, TDestination>();
            return mapper.Map<List<TDestination>>(ds.Tables[tableIndex].CreateDataReader());
        }

        public static List<TDestination> MapToList<TDestination>(this DataSet ds, int tableIndex = 0)
          where TDestination : class
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return default(List<TDestination>);
            var mapper = CreateMapper<IDataReader, TDestination>();
            return mapper.Map<List<TDestination>>(ds.Tables[tableIndex].CreateDataReader());
        }
    }

 


}
