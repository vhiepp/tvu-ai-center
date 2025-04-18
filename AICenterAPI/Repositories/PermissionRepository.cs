using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(MyDBContext context) : base(context)
        {
        }
    }
}
