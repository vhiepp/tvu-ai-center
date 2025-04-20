using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AICenterAPI.Controllers
{
    [Route("api/about")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var about = await _aboutService.About();
            return Ok(new ApiResponse()
            {
                Data = about,
                Success = true,
                Message = "Get about successfully"
            });
        }
    }
}
