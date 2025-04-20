using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AICenterAPI.Controllers
{
    [Route("api/categories")]
    [Route("api/{lang:alpha?}/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService; 

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var language = Thread.CurrentThread.CurrentUICulture.Name;
            var categoryList = await _categoryService.GetAllCategory(language);

            return Ok(
                new ApiResponse()
                {
                     Data = categoryList,
                     Message = "Get all category successfully",
                     Success = true
                }
            );
        }

        [HttpPost]
        [Authorize]
        [Permission("CategoryManager")]
        public async Task<IActionResult> Create(CreateCategoryModel categoryModel)
        {
            var claimUserId = User.FindFirst("Id");
            if (claimUserId == null)
            {
                return Ok(
                    new ApiResponse()
                    {
                        Message = "User not found",
                        Success = false
                    }
                );
            }
            var authorId = int.Parse(claimUserId.Value);
            await _categoryService.CreateCategory(categoryModel, authorId);
            return Ok(
                new ApiResponse()
                {
                    Message = "Create successfully",
                    Success = true
                }
            );
        }

        [HttpGet("{id}")]
        [Authorize]
        [Permission("CategoryManager")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(
                new ApiResponse()
                {
                    Success = true,
                    Message = "Update successfully"
                }
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        [Permission("CategoryManager")]
        public async Task<IActionResult> Update(int id, CreateCategoryModel categoryModel)
        {
            await _categoryService.Update(id, categoryModel);

            return Ok(
                new ApiResponse()
                {
                    Success = true,
                    Message = "Update successfully"
                }
            );
        }
    }
}
