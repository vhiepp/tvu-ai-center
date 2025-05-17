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
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllNews();
            return Ok(new ApiResponse()
            {
                Data = news,
                Success = true,
                Message = "Get all news successfully"
            });
        }

        // GET api/<NewsController>/5
        [HttpGet("detail/{id}")]
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> Get(int id)
        {
            var news = await _newsService.FindDetailUpdateModel(id);
            if (news == null)
            {
                return NotFound(new ApiResponse()
                {
                    Data = null,
                    Success = false,
                    Message = "News not found"
                });
            }
            return Ok(new ApiResponse()
            {
                Data = news,
                Success = true,
                Message = "Get news successfully"
            });
        }

        // POST api/<NewsController>
        [HttpPost]
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> Post([FromForm] CreateNewsModel model)
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
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> Put(int id, [FromForm] CreateNewsModel model)
        {
            await _newsService.UpdateNews(id, model);
            return Ok(
                new ApiResponse()
                {
                    Message = "Update successfully",
                    Success = true,
                    Data = null,
                }
            );
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.Delete(id);
            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Delete successfully"
            });
        }

        // DELETE api/<NewsController>/multiple
        [HttpDelete("multiple")]
        [Authorize]
        [Permission("NewsManager")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            await _newsService.DeleteMultiple(ids);
            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Delete successfully"
            });
        }
    }
}
