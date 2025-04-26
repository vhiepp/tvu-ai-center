using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Partner> _dbSet;
        public PartnerRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Partner>();
        }
    }
}
