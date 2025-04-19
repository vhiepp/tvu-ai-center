using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class NewsCategoryRepository : BaseRepository<NewsCategory>, INewsCategoryRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<NewsCategory> _dbSet;
        public NewsCategoryRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<NewsCategory>();
        }
    }
}
