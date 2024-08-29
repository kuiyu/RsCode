using Microsoft.AspNetCore.Mvc;

namespace Plugin.Doc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocController : ControllerBase
    {
     
        [HttpGet("index")]
        public object Index()
        {
            return new
            {
                title="RsCode�ĵ�",
                time=DateTime.Now,
            };
        }
    }
}
