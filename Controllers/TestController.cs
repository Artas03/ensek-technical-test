using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ensek_Remote_Technical_Test.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("API is alive!");
        }
    }
}
