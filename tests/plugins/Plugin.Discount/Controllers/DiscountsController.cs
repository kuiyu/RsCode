using Microsoft.AspNetCore.Mvc;
using Plugin.Discount.Models;
using System.Diagnostics;

namespace Plugin.Discount.Controllers
{
    public class DiscountsController : Controller
    {
        public IActionResult Index()
        {
            return Content(
                @"this Discounts Index 
                <br/>    
                <p>
                    <a href=""/plugin/Index""> 返回插件管理 </a>
                </p>"
             , "text/html");
        }
    }
}
