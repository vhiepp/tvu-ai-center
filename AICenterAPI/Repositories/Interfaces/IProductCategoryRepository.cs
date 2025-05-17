using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        public Task<List<ProductCategory>> GetByProductId(int productId);

        public Task DeleteByProductId(int id);
    }
}
