using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface INewsContentRepository : IBaseRepository<NewsContent>
    {
        public Task<NewsContent?> FindByNewsIdLanguage(int newsId, string language);

        public Task<List<NewsContent>> GetByNewsId(int newsId);

        public Task DeleteByNewsId(int id);
    }
}
