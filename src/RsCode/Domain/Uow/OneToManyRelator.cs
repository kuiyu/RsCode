using PetaPoco;
using System;
using System.Collections.Generic;

namespace RsCode.Domain.Uow
{

    public class OneToManyRelator<T1, T2>
           where T1 : class
          where T2 : class

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftListFiledName">主表集合字段名称</param> 
        /// <param name="foreignKeyName">辅表与主表关联的字段名称,为空时,值为主表主键</param>
        public OneToManyRelator(string leftListFiledName, string foreignKeyName = "")
        {
            var tableInfo = TableInfo.FromPoco(typeof(T1));
            PrimaryKey = tableInfo.PrimaryKey;

            LeftListFiledName = leftListFiledName;
            if (!string.IsNullOrWhiteSpace(foreignKeyName))
            {
                ForeignKeyName = foreignKeyName;
            }
            else
            {
                ForeignKeyName = PrimaryKey;
            }
        }
        string PrimaryKey = "";
        string LeftListFiledName = "";
        string ForeignKeyName = "";

        public T1 current;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">主表主键类型</typeparam>
        /// <param name="t1">主表</param>
        /// <param name="t2">辅表</param>
        /// <returns></returns>
        public T1 MapIt<T>(T1 t1, T2 t2)
        {
            if (t1 == null)
                return current;

            
            var p1 = t1.GetType().GetProperty(PrimaryKey).GetValue(t1);
            var p2 = current.GetType().GetProperty(PrimaryKey).GetValue(current);

            if (current != null && p1.Equals(p2))
            {
                Type type1 = current.GetType();
                object v1 = Convert.ChangeType(new List<T2>(), type1.GetProperty(LeftListFiledName).PropertyType);
                type1.GetProperty(LeftListFiledName).SetValue(current, v1);

                return null;
            }

            var prev = current;
            current = t1;

            var rhs = new List<T2>();
            //右侧数据是否为空
            if (t2 != null && t2.GetType().GetProperty(ForeignKeyName).GetValue(t2) != null)
            {
                rhs.Add(t2);
            }

            Type type = current.GetType();
            object v = Convert.ChangeType(rhs, type.GetProperty(LeftListFiledName).PropertyType);
            type.GetProperty(LeftListFiledName).SetValue(current, v);

            return prev;
        }


    }
}
