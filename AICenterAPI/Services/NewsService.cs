using AICenterAPI.Datas;
using AICenterAPI.Helpers;
using AICenterAPI.Models;
using AICenterAPI.Repositories;

namespace AICenterAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly IUploadService _uploadService;
        private readonly INewsRepository _newsRepository;
        private readonly INewsContentRepository _newsContentRepository;
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly SlugGenerator _slugGenerator;  

        public NewsService(
            IUploadService uploadService,
            INewsRepository newsRepository,
            INewsContentRepository newsContentRepository,
            INewsCategoryRepository newsCategoryRepository,
            IUserRepository userRepository,
            SlugGenerator slugGenerator
            )
        {
            _uploadService = uploadService;
            _newsRepository = newsRepository;
            _newsContentRepository = newsContentRepository;
            _newsCategoryRepository = newsCategoryRepository;
            _userRepository = userRepository;
            _slugGenerator = slugGenerator;
        }

        public async Task CreateNews(CreateNewsModel model, int userId)
        {
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            var user = await _userRepository.FindByIdAsync(userId);

            var slug = model.Slug;
            var language = model.NewsContents.FirstOrDefault()?.Language;

            if (string.IsNullOrEmpty(slug))
            {
                slug = _slugGenerator.Make(model.NewsContents.FirstOrDefault()?.Title ?? "news");
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

            foreach (var item in model.NewsContents)
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

            foreach (var item in model.Categories)
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
