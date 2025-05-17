using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/education")]
    [Route("api/{lang:alpha?}/education")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        // GET: api/<EducationController>
        [HttpGet]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> GetAll()
        {
            var education = await _educationService.GetAllEducation();
            return Ok(new ApiResponse()
            {
                Data = education,
                Success = true,
                Message = "Get all education successfully"
            });
        }

        // GET api/<EducationController>/5
        [HttpGet("detail/{id}")]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> Get(int id)
        {
            var education = await _educationService.FindDetailUpdateModel(id);
            if (education == null)
            {
                return NotFound(new ApiResponse()
                {
                    Data = null,
                    Success = false,
                    Message = "Education not found"
                });
            }
            return Ok(new ApiResponse()
            {
                Data = education,
                Success = true,
                Message = "Get education successfully"
            });
        }

        // POST api/<EducationController>
        [HttpPost]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> Post([FromForm] CreateEducationModel model)
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
            await _educationService.CreateEducation(model, authorId);

            return Ok(
                new ApiResponse()
                {
                    Message = "Create successfully",
                    Success = true,
                    Data = null,
                }
            );
        }

        // PUT api/<EducationController>/5
        [HttpPut("{id}")]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> Put(int id, [FromForm] CreateEducationModel model)
        {
            await _educationService.UpdateEducation(id, model);
            return Ok(
                new ApiResponse()
                {
                    Message = "Update successfully",
                    Success = true,
                    Data = null,
                }
            );
        }

        // DELETE api/<EducationController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _educationService.Delete(id);
            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Delete successfully"
            });
        }

        // DELETE api/<EducationController>/multiple
        [HttpDelete("multiple")]
        [Authorize]
        [Permission("EducationManager")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            await _educationService.DeleteMultiple(ids);
            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Delete successfully"
            });
        }
    }
}
