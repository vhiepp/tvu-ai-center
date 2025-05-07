using AICenterAPI.Datas;
using AICenterAPI.Models;
using AICenterAPI.Repositories;

namespace AICenterAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryContentRepository _categoryContentRepository;
        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryContentRepository categoryContentRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryContentRepository = categoryContentRepository;
            _userRepository = userRepository;
        }

        public async Task CreateCategory(CreateCategoryModel categoryModel, int authorId)
        {
            var category = new Category()
            {
                AuthorId = authorId
            };
            await _categoryRepository.AddAsync(category);
            foreach (var item in categoryModel.CategoryContents)
            {
                var categoryContent = new CategoryContent()
                {
                    CategoryId = category.Id,
                    Description = item.Description,
                    Language = item.Language,
                    Name = item.Name,
                };
                await _categoryContentRepository.AddAsync(categoryContent);
            }
        }

        public async Task<List<CategoryModel>> GetAllCategory(string? language = "vi")
        {
            if (string.IsNullOrEmpty(language))
            {
                language = "vi";
            }
            var categories = await _categoryRepository.GetAllAsync();
            var categoryList = new List<CategoryModel>();
            foreach (var category in categories)
            {
                var categoryContent = await _categoryContentRepository.FindByCategoryId(category.Id, language);
                var author = await _userRepository.FindByIdAsync(category.AuthorId);
                if (categoryContent != null)
                {
                    var categoryModel = new CategoryModel()
                    {
                        Id = category.Id,
                        Description = categoryContent.Description,
                        Language = categoryContent.Language,
                        Name = categoryContent.Name,
                    };
                    if (author != null) {
                        categoryModel.Author = new UserModel()
                        {
                            FirstName = author.FirstName,
                            LastName = author.LastName,
                            Email = author.Email,
                            Gender = author.Gender,
                            PhoneNumber = author.PhoneNumber
                        };
                    }
                    categoryList.Add(categoryModel);
                }
            }
            return categoryList;
        }

        public async Task<List<CreateCategoryModel>> GetCategoryContentByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int categoryId, CreateCategoryModel categoryModel)
        {
            foreach (var item in categoryModel.CategoryContents)
            {
                var category = new CategoryContent()
                {
                    CategoryId = categoryId,
                    Description = item.Description,
                    Language = item.Language,
                    Name = item.Name,
                };
                await _categoryContentRepository.UpdateAsync(category);
            }
        }

    }
}
