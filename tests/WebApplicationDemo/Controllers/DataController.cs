using Microsoft.AspNetCore.Mvc;
using RsCode;
using RsCode.AspNetCore;

namespace WebApplicationDemo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class DataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpGet("get")]
        public object Data()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();
        }

        [HttpGet("err")]
        public object Error()
        {
            throw new AppException("出错了");
        }
    }
}
