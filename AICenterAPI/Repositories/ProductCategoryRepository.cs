using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<ProductCategory> _dbSet;
        public ProductCategoryRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ProductCategory>();
        }

        public async Task DeleteByProductId(int id)
        {
            var entity = await _dbSet.Where(x => x.ProductId == id).ToListAsync();
            if (entity != null)
            {
                _dbSet.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductCategory>> GetByProductId(int productId)
        {
            return await _dbSet.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
