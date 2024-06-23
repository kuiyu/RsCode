using Microsoft.AspNetCore.Mvc;
using RsCode.AspNetCore;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult err()
        {
            throw new AppException("出错了");
            return View();
        }
        public IActionResult Error()
        { 
            return View();  
        }
    }
}
