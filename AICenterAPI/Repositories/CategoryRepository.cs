using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Category> _dbSet;
        public CategoryRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Category>();
        }
    }
}
