using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Member> _dbSet;
        public MemberRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Member>();
        }
    }
}
