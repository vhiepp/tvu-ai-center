using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface ICategoryContentRepository : IBaseRepository<CategoryContent>
    {
        public Task<List<CategoryContent>> GetByCategoryId(int categoryId);
        public Task<CategoryContent?> FindByCategoryId(int categoryId, string? languge = "vi");

    }
}
