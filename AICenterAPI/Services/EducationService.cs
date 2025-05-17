using System.Text.Json;
using AICenterAPI.Datas;
using AICenterAPI.Helpers;
using AICenterAPI.Models;
using AICenterAPI.Repositories;
using Microsoft.VisualBasic;

namespace AICenterAPI.Services
{
    public class EducationService : IEducationService
    {
        private readonly IUploadService _uploadService;
        private readonly IEducationRepository _educationRepository;
        private readonly IEducationContentRepository _educationContentRepository;
        private readonly IEducationCategoryRepository _educationCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryContentRepository _categoryContentRepository;
        private readonly IUserRepository _userRepository;
        private readonly SlugGenerator _slugGenerator;  

        public EducationService(
            IUploadService uploadService,
            IEducationRepository educationRepository,
            IEducationContentRepository educationContentRepository,
            IEducationCategoryRepository educationCategoryRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICategoryContentRepository categoryContentRepository)
        {
            _uploadService = uploadService;
            _educationRepository = educationRepository;
            _educationContentRepository = educationContentRepository;
            _educationCategoryRepository = educationCategoryRepository;
            _userRepository = userRepository;
            _slugGenerator = new SlugGenerator();
            _categoryRepository = categoryRepository;
            _categoryContentRepository = categoryContentRepository;
        }

        public async Task CreateEducation(CreateEducationModel model, int userId)
        {
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            var user = await _userRepository.FindByIdAsync(userId);
            List<int> categories = new List<int>();
            if (model.Categories != null)
            {
                JsonDocument doc = JsonDocument.Parse($@"{model.Categories}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        categories.Add(element.GetInt32());
                    }
                }
            }
            List<EducationContentModel> contents = new List<EducationContentModel>();
            if (model.EducationContents != null)
            {
                JsonDocument doc = JsonDocument.Parse($@"{model.EducationContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new EducationContentModel()
                        {
                            Title = element.GetProperty("title").GetString(),
                            Language = element.GetProperty("language").GetString(),
                            Content = element.GetProperty("content").GetString(),
                            Slug = element.GetProperty("slug").GetString(),
                        };
                        contents.Add(content);
                    }
                }
            }

            var slug = model.Slug;
            var language = "vi";

            if (contents.Count > 0)
            {
                language = contents[0].Language;
                if (string.IsNullOrEmpty(slug))
                {
                    slug = _slugGenerator.Make(contents[0].Title ?? "education");
                }
            }

            var education = new Education()
            {
                Thumb = thumb,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                IsPublished = model.IsPublished,
                AuthorAvatar = "",
                AuthorName = user.FirstName,
                AuthorId = user.Id,
                AuthorSubtitle = "",
                Slug = slug,
            };

            await _educationRepository.AddAsync(education);

            foreach (var item in contents)
            {
                if (string.IsNullOrEmpty(item.Slug))
                {
                    if (item.Language != language)
                    {
                        item.Slug = _slugGenerator.Make(item.Title ?? "education");
                    }
                    else
                    {
                        item.Slug = slug;
                    }
                }

                var educationContent = new EducationContent()
                {
                    EducationId = education.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _educationContentRepository.AddAsync(educationContent);
            }

            foreach (var item in categories)
            {
                var educationCategory = new EducationCategory()
                {
                    EducationId = education.Id,
                    CategoryId = item,
                };
                await _educationCategoryRepository.AddAsync(educationCategory);
            }
        }

        public async Task Delete(int id)
        {
            var education = await _educationRepository.FindByIdAsync(id);
            if (education == null)
            {
                throw new Exception("Education not found");
            }
            await _educationRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var education = await _educationRepository.FindByIdAsync(id);
                if (education == null)
                {
                    throw new Exception("Education not found");
                }
                await _educationRepository.DeleteByIdAsync(id);
            }
        }

        public async Task<EducationDetailUpdateModel> FindDetailUpdateModel(int id)
        {
            var education = await _educationRepository.FindByIdAsync(id);
            if (education == null)
            {
                throw new Exception("Education not found");
            }
            EducationDetailUpdateModel model = new EducationDetailUpdateModel();
            model.IsPublished = education.IsPublished;
            model.Thumb = education.Thumb;
            var categories = await _educationCategoryRepository.GetByEducationId(id);
            List<int> ids = new List<int>();
            foreach (var item in categories)
            {
                ids.Add(item.CategoryId);
            }
            model.CategoryIds = ids;
            var educationContents = await _educationContentRepository.GetByEducationId(id);
            List<EducationContentModel> contents = new List<EducationContentModel>();
            foreach (var item in educationContents)
            {
                var content = new EducationContentModel()
                {
                    Title = item.Title,
                    Language = item.Language,
                    Content = item.Description,
                    Slug = item.Slug,
                };
                contents.Add(content);
            }
            model.Contents = contents;
            return model;
        }

        public async Task<List<EducationModel>> GetAllEducation(string lang = "vi")
        {
            var education = await _educationRepository.GetAllAsync();
            if (education == null)
                return new List<EducationModel>();
            var educationModels = new List<EducationModel>();
            foreach (var item in education)
            {
                var educationContent = await _educationContentRepository.FindByEducationIdLanguage(item.Id, lang);
                if (educationContent == null)
                    continue;
                var categoryIds = await _educationCategoryRepository.GetByEducationId(item.Id);
                var categoryModels = new List<CategoryModel>();
                foreach (var categoryId in categoryIds)
                {
                    var category = await _categoryRepository.FindByIdAsync(categoryId.CategoryId);
                    if (category == null)
                        continue;
                    var categoryContent = await _categoryContentRepository.FindByCategoryId(category.Id, lang);
                    if (categoryContent == null)
                        continue;
                    var categoryModel = new CategoryModel()
                    {
                        Id = category.Id,
                        Name = categoryContent.Name,
                        Language = categoryContent.Language,
                        Description = categoryContent.Description,
                    };
                    categoryModels.Add(categoryModel);
                }
                var educationModel = new EducationModel()
                {
                    Id = item.Id,
                    Thumb = item.Thumb,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    PublishedAt = item.PublishedAt,
                    IsPublished = item.IsPublished,
                    AuthorAvatar = item.AuthorAvatar,
                    AuthorName = item.AuthorName,
                    AuthorSubtitle = item.AuthorSubtitle,
                    Slug = educationContent.Slug,
                    Categories = categoryModels,
                    Content = new EducationContentModel()
                    {
                        Title = educationContent.Title,
                        Language = educationContent.Language,
                        Content = educationContent.Description,
                        Slug = educationContent.Slug,
                    },
                };
                educationModels.Add(educationModel);
            }
            return educationModels;
        }

        public async Task UpdateEducation(int id, CreateEducationModel model)
        {
            var education = await _educationRepository.FindByIdAsync(id);
            if (education == null)
            {
                throw new Exception("Education not found");
            }
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            List<int> categories = new List<int>();
            if (model.Categories != null)
            {
                await _educationCategoryRepository.DeleteByEducationId(id);
                JsonDocument doc = JsonDocument.Parse($@"{model.Categories}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        categories.Add(element.GetInt32());
                    }
                }
            }
            List<EducationContentModel> contents = new List<EducationContentModel>();
            if (model.EducationContents != null)
            {
                await _educationContentRepository.DeleteByEducationId(id);
                JsonDocument doc = JsonDocument.Parse($@"{model.EducationContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new EducationContentModel()
                        {
                            Title = element.GetProperty("title").GetString(),
                            Language = element.GetProperty("language").GetString(),
                            Content = element.GetProperty("content").GetString(),
                            Slug = element.GetProperty("slug").GetString(),
                        };
                        contents.Add(content);
                    }
                }
            }
            

            education.Thumb = thumb;
            education.UpdatedAt = DateTime.Now;
            education.IsPublished = model.IsPublished;
            if (!education.IsPublished && model.IsPublished)
            {
                education.PublishedAt = DateTime.Now;
                
            }

            await _educationRepository.UpdateAsync(education);

            foreach (var item in contents)
            {
                var educationContent = new EducationContent()
                {
                    EducationId = education.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _educationContentRepository.AddAsync(educationContent);
            }

            foreach (var item in categories)
            {
                var educationCategory = new EducationCategory()
                {
                    EducationId = education.Id,
                    CategoryId = item,
                };
                await _educationCategoryRepository.AddAsync(educationCategory);
            }
        }
    }
}
