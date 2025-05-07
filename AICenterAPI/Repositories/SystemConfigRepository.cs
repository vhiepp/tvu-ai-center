using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class SystemConfigRepository : BaseRepository<SystemConfig>, ISystemConfigRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<SystemConfig> _dbSet;
        public SystemConfigRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<SystemConfig>();
        }
    }
}
