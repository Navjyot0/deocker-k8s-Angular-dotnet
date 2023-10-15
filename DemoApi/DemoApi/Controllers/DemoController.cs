using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("demo")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {
            return "Hello World";
        }
    }
}
