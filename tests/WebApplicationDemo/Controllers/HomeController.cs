using MediatR;
using Microsoft.AspNetCore.Mvc;
using RsCode;
using RsCode.AspNetCore;
using RsCode.AspNetCore.Plugin;
using RsCode.Domain;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {

        IUserTestService userTestService;
        public HomeController(IUserTestService userTestService)
        {
          this.userTestService = userTestService;
        }
       
        public IActionResult Index()
        {
            return View();
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
