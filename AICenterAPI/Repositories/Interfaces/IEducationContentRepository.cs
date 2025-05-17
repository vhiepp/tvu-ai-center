using AICenterAPI.Datas;

namespace AICenterAPI.Repositories
{
    public interface IEducationContentRepository : IBaseRepository<EducationContent>
    {
        public Task<EducationContent?> FindByEducationIdLanguage(int educationId, string language);

        public Task<List<EducationContent>> GetByEducationId(int educationId);

        public Task DeleteByEducationId(int id);
    }
}
