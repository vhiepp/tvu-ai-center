using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Education> _dbSet;
        public EducationRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Education>();
        }
    }
}
