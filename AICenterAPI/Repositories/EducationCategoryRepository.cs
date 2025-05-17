using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class EducationCategoryRepository : BaseRepository<EducationCategory>, IEducationCategoryRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<EducationCategory> _dbSet;
        public EducationCategoryRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EducationCategory>();
        }

        public async Task DeleteByEducationId(int id)
        {
            var entity = await _dbSet.Where(x => x.EducationId == id).ToListAsync();
            if (entity != null)
            {
                _dbSet.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EducationCategory>> GetByEducationId(int educationId)
        {
            return await _dbSet.Where(x => x.EducationId == educationId).ToListAsync();
        }
    }
}
