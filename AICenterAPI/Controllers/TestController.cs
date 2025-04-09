using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AICenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new {
                Test = "hello"
            });
        }
    }
}
