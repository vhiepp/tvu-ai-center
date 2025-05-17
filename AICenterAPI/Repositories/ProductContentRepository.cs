using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class ProductContentRepository : BaseRepository<ProductContent>, IProductContentRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<ProductContent> _dbSet;
        public ProductContentRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ProductContent>();
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

        public async Task<ProductContent?> FindByProductIdLanguage(int productId, string language)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == productId && x.Language == language);
        }

        public async Task<List<ProductContent>> GetByProductId(int productId)
        {
            return await _dbSet.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
