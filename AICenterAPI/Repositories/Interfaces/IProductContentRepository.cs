using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IProductContentRepository : IBaseRepository<ProductContent>
    {
        public Task<ProductContent?> FindByProductIdLanguage(int productId, string language);

        public Task<List<ProductContent>> GetByProductId(int productId);

        public Task DeleteByProductId(int id);
    }
}
