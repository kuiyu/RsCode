using MediatR;
using Microsoft.AspNetCore.Mvc;
using RsCode.AspNetCore;
using RsCode.Domain;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        IMediator mediator;
        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            await mediator.Publish(new UserCreateEvent("test"));
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
