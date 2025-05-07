using AICenterAPI.Models;
using AICenterAPI.Repositories;

namespace AICenterAPI.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly IPageContentRepository _pageContentRepository;

        public PageContentService(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task<PageContentModel?> FindByKeyLanguageAsync(string key, string language)
        {
            var pageContent = await _pageContentRepository.FindByKeyLanguage(key, language);
            if (pageContent == null)
                return null;
            return new PageContentModel
            {
                Key = pageContent.Key,
                Language = pageContent.Language,
                Content = pageContent.Content
            };
        }

        public async Task<List<PageContentModel>> GetByKeyAsync(string key)
        {
            var pageContents = await _pageContentRepository.GetByKey(key);
            if (pageContents == null)
                return new List<PageContentModel>();
            return pageContents.Select(x => new PageContentModel
            {
                Key = x.Key,
                Language = x.Language,
                Content = x.Content
            }).ToList();
        }

        public async Task UpdateByKeyAsync(string key, List<UpdatePageContentModel> lists)
        {
            var pageContents = await _pageContentRepository.GetByKey(key);
            if (pageContents == null)
                return;
            foreach (var pageContent in pageContents)
            {
                var newPageContent = lists.FirstOrDefault(x => x.Language == pageContent.Language);
                if (newPageContent != null)
                {
                    pageContent.Content = newPageContent.Content;
                    await _pageContentRepository.UpdateAsync(pageContent);
                }
            }
        }
    }
}
