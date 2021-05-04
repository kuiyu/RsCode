using System.Collections.Concurrent;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class PropertyValue<T> where T : class
    {
        private static ConcurrentDictionary<string, MemberGetDelegate> _memberGetDelegate = new ConcurrentDictionary<string, MemberGetDelegate>();
        delegate object MemberGetDelegate(T obj);
        public PropertyValue(T obj)
        {
            Target = obj;
        }
        public T Target { get; private set; }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  object Get(string name)
        {
            MemberGetDelegate memberGet = _memberGetDelegate.GetOrAdd(name, BuildDelegate);
            return memberGet(Target);
        }
        private MemberGetDelegate BuildDelegate(string name)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(name);
            return (MemberGetDelegate)Delegate.CreateDelegate(typeof(MemberGetDelegate), property.GetGetMethod());
        }
    }
}
