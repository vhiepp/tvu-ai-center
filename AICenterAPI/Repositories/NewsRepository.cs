using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<News> _dbSet;
        public NewsRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<News>();
        }
    }
}
