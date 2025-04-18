using Microsoft.EntityFrameworkCore;
using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Province> _dbSet;
        public ProvinceRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Province>();
        }
    }
}
