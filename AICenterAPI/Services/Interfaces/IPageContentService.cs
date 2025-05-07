using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IPageContentService
    {
        public Task<List<PageContentModel>> GetByKeyAsync(string key);

        public Task<PageContentModel?> FindByKeyLanguageAsync(string key, string language);

        public Task UpdateByKeyAsync(string key, List<UpdatePageContentModel> lists);
    }
}
