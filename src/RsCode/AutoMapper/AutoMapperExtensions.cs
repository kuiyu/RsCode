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
using AutoMapper;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace System
{
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object Sync = new object();

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
             
            if (source == null)
                throw new Exception($"use MapTo<>() error:{nameof(source)} is null" );
            if (destination == null)
                throw new Exception($"use MapTo<>() error:{nameof(destination)} is null");
            var sourceType = GetType(source);
            var destinationType = GetType(destination);
            var map = GetMap(sourceType, destinationType);
            
            if (map != null)
                return Mapper.Map(source, destination);
            lock (Sync)
            {
                map = GetMap(sourceType, destinationType);
                if (map != null)
                    return Mapper.Map(source, destination);
                InitMaps(sourceType, destinationType);
            }
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        private static Type GetType(object obj)
        {
            var type = obj.GetType();
            if ((obj is System.Collections.IEnumerable) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException("泛型类型参数不能为空");
            return genericArgumentsTypes[0];
        }

        /// <summary>
        /// 获取映射配置
        /// </summary>
        private static TypeMap GetMap(Type sourceType, Type destinationType)
        {
            try
            {
                return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
            }
            catch (InvalidOperationException)
            {
                lock (Sync)
                {
                    try
                    {
                        return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                    }
                    catch (InvalidOperationException)
                    {
                        InitMaps(sourceType, destinationType);
                    }
                    return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                }
            }
        }



        /// <summary>
        /// 初始化映射配置
        /// </summary>
        private static void InitMaps(Type sourceType, Type destinationType)
        {
            try
            {
                var maps = Mapper.Configuration.GetAllTypeMaps();
                Mapper.Initialize(config => {
                    ClearConfig();
                    foreach (var item in maps)
                    {
                          config.CreateMap(item.SourceType, item.DestinationType)
                        //.ForMember(item.DestinationType.Name,
                        //dest=> {
                        //    dest.Ignore();
                        //})
                        ;
                         
                    }
                        
                    config.CreateMap(sourceType, destinationType);
                });
            }
            catch (InvalidOperationException)
            {
                Mapper.Initialize(config => {
                    config.CreateMap(sourceType, destinationType);
                });
            }
        }

        /// <summary>
        /// 清空配置
        /// </summary>
        private static void ClearConfig()
        {
            var typeMapper = typeof(Mapper).GetTypeInfo();
            var configuration = typeMapper.GetDeclaredField("_configuration");
            configuration.SetValue(null, null, BindingFlags.Static, null, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 将源集合映射到目标集合
        /// </summary>
        /// <typeparam name="TDestination">目标元素类型,范例：Sample,不要加List</typeparam>
        /// <param name="source">源集合</param>
        public static List<TDestination> MapToList<TDestination>(this System.Collections.IEnumerable source)
        {
            return MapTo<List<TDestination>>(source);
        }
    }
}
