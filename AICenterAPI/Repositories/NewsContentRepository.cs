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

        public async Task DeleteByNewsId(int id)
        {
            var entity = await _dbSet.Where(x => x.NewsId == id).ToListAsync();
            if (entity != null)
            {
                _dbSet.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<NewsContent?> FindByNewsIdLanguage(int newsId, string language)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.NewsId == newsId && x.Language == language);
        }

        public async Task<List<NewsContent>> GetByNewsId(int newsId)
        {
            return await _dbSet.Where(x => x.NewsId == newsId).ToListAsync();
        }
    }
}
