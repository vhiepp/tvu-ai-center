using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/page-contents")]
    [Route("api/{lang:alpha?}/page-contents")]
    [ApiController]
    public class PageContentController : ControllerBase
    {
        private readonly IPageContentService _pageContentService;
        public PageContentController(IPageContentService pageContentService)
        {
            _pageContentService = pageContentService;
        }

        // GET: api/<PageContentController>
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
            //var pageContents = await _pageContentService.GetAll();
            //if (pageContents == null || pageContents.Count == 0)
            //{
            //    return NotFound();
            //}
            //return Ok(pageContents);
        //}

        // GET api/<PageContentController>/5
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var culture = Thread.CurrentThread.CurrentUICulture.Name;

            if (culture != null && culture.Length > 0)
            {
                var pageContent = await _pageContentService.FindByKeyLanguageAsync(key, culture);

                if (pageContent != null)
                {
                    return Ok(new ApiResponse()
                    {
                        Data = pageContent,
                        Message = "Get page content by key successfully",
                        Success = true
                    });
                }
            }
            var pageContents = await _pageContentService.GetByKeyAsync(key);
            if (pageContents != null && pageContents.Count > 0)
            {
                return Ok(new ApiResponse()
                {
                    Data = pageContents,
                    Message = "Get page contents by key successfully",
                    Success = true
                });
            }
            return NotFound();
        }

        // POST api/<PageContentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PageContentController>/5
        [HttpPut("{key}")]
        [Authorize]
        [Permission("AboutManager")]
        public async Task<IActionResult> Put(string key, List<UpdatePageContentModel> lists)
        {
            if (lists == null || lists.Count == 0)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = "Page content list is empty",
                    Success = false
                });
            }
            await _pageContentService.UpdateByKeyAsync(key, lists);
            return Ok(new ApiResponse()
            {
                Message = "Update page content successfully",
                Success = true
            });
        }

        // DELETE api/<PageContentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
