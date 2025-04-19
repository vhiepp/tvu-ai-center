using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class NewsContentRepository : BaseRepository<NewsContent>, INewsContentRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<NewsContent> _dbSet;
        public NewsContentRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<NewsContent>();
        }
    }
}
