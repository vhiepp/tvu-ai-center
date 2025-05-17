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

        public async Task DeleteByNewsId(int id)
        {
            var entity = await _dbSet.Where(x => x.NewsId == id).ToListAsync();
            if (entity != null)
            {
                _dbSet.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NewsCategory>> GetByNewsId(int newsId)
        {
            return await _dbSet.Where(x => x.NewsId == newsId).ToListAsync();
        }
    }
}
