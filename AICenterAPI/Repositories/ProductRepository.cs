using AICenterAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public readonly MyDBContext _context;
        public readonly DbSet<Product> _dbSet;
        public ProductRepository(MyDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }
    }
}
