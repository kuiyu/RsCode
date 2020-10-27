using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.AspNetCore.XSS
{
   public class XssHelper
    {
        HtmlSanitizer sanitizer;
        public XssHelper()
        {
            sanitizer = new HtmlSanitizer();
            //sanitizer.AllowedTags.Add("div");//标签白名单
            sanitizer.AllowedAttributes.Add("class");//标签属性白名单,默认没有class标签属性           
            //sanitizer.AllowedCssProperties.Add("font-family");//CSS属性白名单
        }

        /// <summary>
        /// XSS过滤
        /// </summary>
        /// <param name="html">html代码</param>
        /// <returns>过滤结果</returns>
        public string Filter(string html)
        {
            string str = sanitizer.Sanitize(html);
            return str;
        }
    }
}
