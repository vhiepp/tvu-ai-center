using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IPageContentRepository : IBaseRepository<PageContent>
    {
        public Task<List<PageContent>> GetByKey(string key);
        public Task<PageContent?> FindByKeyLanguage(string key, string language);
    }
}
