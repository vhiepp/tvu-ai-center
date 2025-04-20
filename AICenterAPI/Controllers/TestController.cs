using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AICenterAPI.Controllers
{
    [Route("api/tests")]
    [Route("api/{lang:alpha?}/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var culture = Thread.CurrentThread.CurrentUICulture.Name;

            var result = culture switch
            {
                "en" => new { Title = "Hello", Content = "This is English content." },
                "vi" => new { Title = "Xin chào", Content = "Đây là nội dung tiếng Việt." }
            };

            return Ok(result);
        }
    }
}
