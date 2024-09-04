/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */


using Microsoft.AspNetCore.Mvc.Filters;
using RsCode.AspNetCore.XSS;
using System;

namespace RsCode.AspNetCore
{
    public class AntiXSSAttribute : Attribute, IActionFilter
    {
        private XssHelper xss;
        public AntiXSSAttribute()
        {
            xss = new XssHelper();
        }

       
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //取消204 预检请求
            //context.HttpContext.Request.Headers.Add("Access-Control-Allow-Headers", "*");
            //context.HttpContext.Request.Headers.Add("Access-Control-Allow-Methods", "*");
            //context.HttpContext.Request.Headers.Add("Access-Control-Max-Age", new Microsoft.Extensions.Primitives.StringValues("24*60*60"));
        }

        //在调用Action方法之前调用
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //获取Action参数集合
            var ps = context.ActionDescriptor.Parameters;
            //遍历参数集合
            foreach (var p in ps)
            {
                if(context.ActionArguments.ContainsKey(p.Name))
                {
                    if (context.ActionArguments[p.Name] != null)
                    {
                        //当参数等于字符串
                        if (p.ParameterType.Equals(typeof(string)))
                        {
                            context.ActionArguments[p.Name] = xss.Filter(context.ActionArguments[p.Name].ToString());
                        }
                        //else if (p.ParameterType.IsClass)//当参数等于类
                        //{
                        //    ModelFieldFilter(p.Name, p.ParameterType, context.ActionArguments[p.Name]);
                        //}
                    }
                } 
            }
        }

        /// <summary>
        /// 遍历修改类的字符串属性
        /// </summary>
        /// <param name="key">类名</param>
        /// <param name="t">数据类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private object ModelFieldFilter(string key, Type t, object obj)
        {
            //获取类的属性集合
            //var ats = t.GetCustomAttributes(typeof(FieldFilterAttribute), false);


            if (obj != null)
            {
                //获取类的属性集合
                var pps = t.GetProperties();

                foreach (var pp in pps)
                {
                    if (pp.GetValue(obj) != null)
                    {
                        //当属性等于字符串
                        if (pp.PropertyType.Equals(typeof(string)))
                        {
                            string value = pp.GetValue(obj).ToString();
                            pp.SetValue(obj, xss.Filter(value));
                        }
                        if(pp.PropertyType.IsArray)
                        {
                            var value = pp.GetValue(obj);
                            pp.SetValue(obj, value);
                        }else if (pp.PropertyType.IsClass)//当属性等于类进行递归
                        {
                            pp.SetValue(obj, ModelFieldFilter(pp.Name, pp.PropertyType, pp.GetValue(obj)));
                        }
                      
                    }

                }
            }

            return obj;
        }
    }
}
