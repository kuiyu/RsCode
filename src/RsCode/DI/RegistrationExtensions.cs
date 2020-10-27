using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RsCode.DI
{
    internal static class RegistrationUtils
    {
       
        /// <summary>
        /// 获取指定类型的接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type[]GetImplementedInterfaces(Type type)
        {
            var interfaces = type.GetTypeInfo().ImplementedInterfaces.Where(i => i != typeof(IDisposable));
            return type.GetTypeInfo().IsInterface ? interfaces.AppendItem(type).ToArray() : interfaces.ToArray();
        }

        /// <summary>
        /// 从指定程序集中查询指定接口的所有实现类
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static  Type[]GetImplemented<T>(Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes()
            .Where(t=>t.GetInterfaces().Contains(typeof(T))
            
            ))
            .ToArray();
        }

        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }

    internal static class SequenceExtensions
    {
        /// <summary>
        /// Appends the item to the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="trailingItem">The trailing item.</param>
        /// <returns>The sequence with an item appended to the end.</returns>
        public static IEnumerable<T> AppendItem<T>(this IEnumerable<T> sequence, T trailingItem)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            foreach (var t in sequence)
                yield return t;

            yield return trailingItem;
        }
    }
}
