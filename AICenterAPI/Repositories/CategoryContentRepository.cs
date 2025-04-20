using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class CategoryContentRepository : BaseRepository<CategoryContent>, ICategoryContentRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<CategoryContent> _dbSet;
        public CategoryContentRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<CategoryContent>();
        }

        public async Task<CategoryContent?> FindByCategoryId(int categoryId, string? languge = "vi")
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.Language == languge);
        }

        public async Task<List<CategoryContent>> GetByCategoryId(int categoryId)
        {
            return await _dbSet.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
