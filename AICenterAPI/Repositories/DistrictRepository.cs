using Microsoft.EntityFrameworkCore;
using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<District> _dbSet;
        public DistrictRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<District>();
        }

        public async Task<List<District>> GetDistrictByProvinceId(int provinceId)
        {
            return await _dbSet.Where(x => x.ProvinceId == provinceId).ToListAsync();
        }
    }
}
