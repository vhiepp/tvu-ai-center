using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class EducationContentRepository : BaseRepository<EducationContent>, IEducationContentRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<EducationContent> _dbSet;
        public EducationContentRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EducationContent>();
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

        public async Task<EducationContent?> FindByEducationIdLanguage(int educationId, string language)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.EducationId == educationId && x.Language == language);
        }

        public async Task<List<EducationContent>> GetByEducationId(int educationId)
        {
            return await _dbSet.Where(x => x.EducationId == educationId).ToListAsync();
        }
    }
}
