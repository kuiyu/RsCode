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
                    <a href=""/plugin/Index"">  back  plugin manager </a>
                </p>"
             , "text/html");
        }
    }
}
