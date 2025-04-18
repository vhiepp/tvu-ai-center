using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IWardRepository : IBaseRepository<Ward>
    {
        public Task<List<Ward>> GetWardByDistrictId(int districtId);
    }
}
