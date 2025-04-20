using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface ICategoryService
    {
        public Task CreateCategory(CreateCategoryModel categoryModel, int authorId);

        public Task<List<CategoryModel>> GetAllCategory(string? language = "vi");

        public Task Update(int categoryId, CreateCategoryModel categoryModel);

        public Task<List<CreateCategoryModel>> GetCategoryContentByCategoryId(int categoryId);
    }
}
