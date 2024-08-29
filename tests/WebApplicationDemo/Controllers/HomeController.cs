using MediatR;
using Microsoft.AspNetCore.Mvc;
using RsCode.AspNetCore;
using RsCode.AspNetCore.Plugin;
using RsCode.Domain;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
     
    
        public HomeController()
        {
          
        }
      

        public IActionResult err()
        {
            throw new AppException("出错了");
           
        }
        public IActionResult Error()
        { 
            return View();  
        }
    }
}
