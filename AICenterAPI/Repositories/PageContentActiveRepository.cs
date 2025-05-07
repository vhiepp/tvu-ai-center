using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class PageContentActiveRepository : BaseRepository<PageContentActive>, IPageContentActiveRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<PageContentActive> _dbSet;
        public PageContentActiveRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PageContentActive>();
        }
    }
}
