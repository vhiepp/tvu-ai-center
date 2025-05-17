using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IEducationCategoryRepository : IBaseRepository<EducationCategory>
    {
        public Task<List<EducationCategory>> GetByEducationId(int educationId);

        public Task DeleteByEducationId(int id);
    }
}
