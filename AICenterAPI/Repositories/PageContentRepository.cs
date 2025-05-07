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

        public async Task<PageContent?> FindByKeyLanguage(string key, string language)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Key == key && x.Language == language);
        }

        public async Task<List<PageContent>> GetByKey(string key)
        {
            return await _dbSet.Where(x => x.Key == key).ToListAsync();
        }
    }
}
