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
    }
}
