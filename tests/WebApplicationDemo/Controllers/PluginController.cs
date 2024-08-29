using Microsoft.AspNetCore.Mvc;
using RsCode.AspNetCore;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class PluginController : Controller
    {
        IPluginManager pluginManager;
        public PluginController(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var plugins =pluginManager.GetAllPlugins<PluginModel>();
            return Content(System.Text.Json.JsonSerializer.Serialize(plugins));
        }
        
        public IActionResult disable(string pluginName)
        { 
            pluginManager.DisablePlugin(pluginName);
            return Content("ok");
        }
       
        public IActionResult enable(string pluginName)
        { 
            pluginManager.EnablePlugin(pluginName);
            return Content("ok");
        }

    }
}
