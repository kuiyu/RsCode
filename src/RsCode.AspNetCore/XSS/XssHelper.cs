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
