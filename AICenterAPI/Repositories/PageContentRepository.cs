using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class PageContentRepository : BaseRepository<PageContent>, IPageContentRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<PageContent> _dbSet;
        public PageContentRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PageContent>();
        }
    }
}
