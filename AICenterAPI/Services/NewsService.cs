using System.Text.Json;
using AICenterAPI.Datas;
using AICenterAPI.Helpers;
using AICenterAPI.Models;
using AICenterAPI.Repositories;
using Microsoft.VisualBasic;

namespace AICenterAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly IUploadService _uploadService;
        private readonly INewsRepository _newsRepository;
        private readonly INewsContentRepository _newsContentRepository;
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryContentRepository _categoryContentRepository;
        private readonly IUserRepository _userRepository;
        private readonly SlugGenerator _slugGenerator;  

        public NewsService(
            IUploadService uploadService,
            INewsRepository newsRepository,
            INewsContentRepository newsContentRepository,
            INewsCategoryRepository newsCategoryRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICategoryContentRepository categoryContentRepository)
        {
            _uploadService = uploadService;
            _newsRepository = newsRepository;
            _newsContentRepository = newsContentRepository;
            _newsCategoryRepository = newsCategoryRepository;
            _userRepository = userRepository;
            _slugGenerator = new SlugGenerator();
            _categoryRepository = categoryRepository;
            _categoryContentRepository = categoryContentRepository;
        }

        public async Task CreateNews(CreateNewsModel model, int userId)
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
            List<NewsContentModel> contents = new List<NewsContentModel>();
            if (model.NewsContents != null)
            {
                JsonDocument doc = JsonDocument.Parse($@"{model.NewsContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new NewsContentModel()
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
                    slug = _slugGenerator.Make(contents[0].Title ?? "news");
                }
            }

            var news = new News()
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

            await _newsRepository.AddAsync(news);

            foreach (var item in contents)
            {
                if (string.IsNullOrEmpty(item.Slug))
                {
                    if (item.Language != language)
                    {
                        item.Slug = _slugGenerator.Make(item.Title ?? "news");
                    }
                    else
                    {
                        item.Slug = slug;
                    }
                }

                var newsContent = new NewsContent()
                {
                    NewsId = news.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _newsContentRepository.AddAsync(newsContent);
            }

            foreach (var item in categories)
            {
                var newsCategory = new NewsCategory()
                {
                    NewsId = news.Id,
                    CategoryId = item,
                };
                await _newsCategoryRepository.AddAsync(newsCategory);
            }
        }

        public async Task Delete(int id)
        {
            var news = await _newsRepository.FindByIdAsync(id);
            if (news == null)
            {
                throw new Exception("News not found");
            }
            await _newsRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var news = await _newsRepository.FindByIdAsync(id);
                if (news == null)
                {
                    throw new Exception("News not found");
                }
                await _newsRepository.DeleteByIdAsync(id);
            }
        }

        public async Task<NewsDetailUpdateModel> FindDetailUpdateModel(int id)
        {
            var news = await _newsRepository.FindByIdAsync(id);
            if (news == null)
            {
                throw new Exception("News not found");
            }
            NewsDetailUpdateModel model = new NewsDetailUpdateModel();
            model.IsPublished = news.IsPublished;
            model.Thumb = news.Thumb;
            var categories = await _newsCategoryRepository.GetByNewsId(id);
            List<int> ids = new List<int>();
            foreach (var item in categories)
            {
                ids.Add(item.CategoryId);
            }
            model.CategoryIds = ids;
            var newsContents = await _newsContentRepository.GetByNewsId(id);
            List<NewsContentModel> contents = new List<NewsContentModel>();
            foreach (var item in newsContents)
            {
                var content = new NewsContentModel()
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

        public async Task<List<NewsModel>> GetAllNews(string lang = "vi")
        {
            var news = await _newsRepository.GetAllAsync();
            if (news == null)
                return new List<NewsModel>();
            var newsModels = new List<NewsModel>();
            foreach (var item in news)
            {
                var newsContent = await _newsContentRepository.FindByNewsIdLanguage(item.Id, lang);
                if (newsContent == null)
                    continue;
                var categoryIds = await _newsCategoryRepository.GetByNewsId(item.Id);
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
                var newsModel = new NewsModel()
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
                    Slug = newsContent.Slug,
                    Categories = categoryModels,
                    Content = new NewsContentModel()
                    {
                        Title = newsContent.Title,
                        Language = newsContent.Language,
                        Content = newsContent.Description,
                        Slug = newsContent.Slug,
                    },
                };
                newsModels.Add(newsModel);
            }
            return newsModels;
        }

        public async Task UpdateNews(int id, CreateNewsModel model)
        {
            var news = await _newsRepository.FindByIdAsync(id);
            if (news == null)
            {
                throw new Exception("News not found");
            }
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            List<int> categories = new List<int>();
            if (model.Categories != null)
            {
                await _newsCategoryRepository.DeleteByNewsId(id);
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
            List<NewsContentModel> contents = new List<NewsContentModel>();
            if (model.NewsContents != null)
            {
                await _newsContentRepository.DeleteByNewsId(id);
                JsonDocument doc = JsonDocument.Parse($@"{model.NewsContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new NewsContentModel()
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
            

            news.Thumb = thumb;
            news.UpdatedAt = DateTime.Now;
            news.IsPublished = model.IsPublished;
            if (!news.IsPublished && model.IsPublished)
            {
                news.PublishedAt = DateTime.Now;
                
            }

            await _newsRepository.UpdateAsync(news);

            foreach (var item in contents)
            {
                var newsContent = new NewsContent()
                {
                    NewsId = news.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _newsContentRepository.AddAsync(newsContent);
            }

            foreach (var item in categories)
            {
                var newsCategory = new NewsCategory()
                {
                    NewsId = news.Id,
                    CategoryId = item,
                };
                await _newsCategoryRepository.AddAsync(newsCategory);
            }
        }
    }
}
