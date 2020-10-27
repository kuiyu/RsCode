using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 枚举类型工具类
    /// </summary>
    public static class EnumHelper
    {
        private static Hashtable enumDesciption;

        static EnumHelper()
        {
            EnumHelper.enumDesciption = EnumHelper.GetDescriptionContainer();
        }

        private static void AddToEnumDescription(Type enumType)
        {
            EnumHelper.enumDesciption.Add(enumType, EnumHelper.GetEnumDic(enumType));
        }

        private static string GetDescription(Type enumType, string enumText)
        {
            string item;
            if (!string.IsNullOrEmpty(enumText))
            {
                if (!EnumHelper.enumDesciption.ContainsKey(enumType))
                {
                    EnumHelper.AddToEnumDescription(enumType);
                }
                object obj = EnumHelper.enumDesciption[enumType];
                if ((obj == null ? true : string.IsNullOrEmpty(enumText)))
                {
                    throw new ApplicationException("不存在枚举的描述");
                }
                item = ((Dictionary<string, string>)obj)[enumText];
            }
            else
            {
                item = null;
            }
            return item;
        }

        private static Hashtable GetDescriptionContainer()
        {
            EnumHelper.enumDesciption = new Hashtable();
            return EnumHelper.enumDesciption;
        }

        private static Dictionary<string, string> GetEnumDic(Type enumType)
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            FieldInfo[] fields = enumType.GetFields();

            for (int i = 0; i < fields.Length; i++)
            {

                FieldInfo fieldInfo = fields[i];
                if (fieldInfo.FieldType.IsEnum)
                {
                    object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (customAttributes.Length > 0)
                        strs.Add(fieldInfo.Name, ((DescriptionAttribute)customAttributes[0]).Description);
                }
            }
            return strs;
        }

        private static bool IsIntType(double d)
        {
            return (int)d != d;
        }

        public static string ToDescription(this Enum value)
        {
            string description;
            if (value != null)
            {
                Type type = value.GetType();
                string name = Enum.GetName(type, value);
                description = EnumHelper.GetDescription(type, name);
            }
            else
            {
                description = "";
            }
            return description;
        }

     
        public static Dictionary<int, string> ToDescriptionDictionary<TEnum>()
        {
            Array values = Enum.GetValues(typeof(TEnum));
            Dictionary<int, string> nums = new Dictionary<int, string>();
            foreach (Enum value in values)
            {
                nums.Add(Convert.ToInt32(value), value.ToDescription());
            }
            return nums;
        }

        public static Dictionary<int, string> ToDictionary<TEnum>()
        {
            Array values = Enum.GetValues(typeof(TEnum));
            Dictionary<int, string> nums = new Dictionary<int, string>();
            foreach (Enum value in values)
            {
                nums.Add(Convert.ToInt32(value), value.ToString());
            }
            return nums;
        }

        public static TEnum Parse<TEnum>(string value)
            where TEnum:struct
        {
            TEnum ret;
            Enum.TryParse<TEnum>(value, out ret);
            return ret;
        }

        /// <summary>
        /// 返回枚举值的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举值。</param>
        /// <returns>枚举值的描述信息。</returns>
        public static string GetEnumDesc<T>(int value)
        {
            Type enumType = typeof(T);
            DescriptionAttribute attr = null;

            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                }
            }

            // 返回结果
            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;
        }

    }


}
 
