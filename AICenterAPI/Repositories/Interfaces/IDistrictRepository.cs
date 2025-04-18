using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IDistrictRepository : IBaseRepository<District>
    {
        public Task<List<District>> GetDistrictByProvinceId(int provinceId);
    }
}
