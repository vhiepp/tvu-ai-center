using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/news")]
    [Route("api/{lang:alpha?}/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/<NewsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewsController>
        [HttpPost]
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> Post([FromBody] CreateNewsModel model)
        {
            var claimUserId = User.FindFirst("Id");
            if (claimUserId == null)
            {
                return Ok(
                    new ApiResponse()
                    {
                        Message = "User not found",
                        Success = false,
                        Data = null,
                    }
                );
            }
            var authorId = int.Parse(claimUserId.Value);
            await _newsService.CreateNews(model, authorId);

            return Ok(
                new ApiResponse()
                {
                    Message = "Create successfully",
                    Success = true,
                    Data = null,
                }
            );
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
