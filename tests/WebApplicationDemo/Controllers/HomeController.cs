using DeepSeek.Core;
using DeepSeek.Core.Adapters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
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
        IChatClient chatClient;
        DeepSeekClient client;
        public HomeController(IUserTestService userTestService, IChatClient chatClient)
        {
          this.userTestService = userTestService;
            this.chatClient = chatClient;
             
        }
       
        public async Task<IActionResult> Index()
        {
            var cancelToken = new CancellationTokenSource();
            var t=chatClient.AsBuilder().Build();
             
            var models= await client.ListModelsAsync(cancelToken.Token);
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
