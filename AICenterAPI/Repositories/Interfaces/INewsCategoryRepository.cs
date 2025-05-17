using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface INewsCategoryRepository : IBaseRepository<NewsCategory>
    {
        public Task<List<NewsCategory>> GetByNewsId(int newsId);

        public Task DeleteByNewsId(int id);
    }
}
