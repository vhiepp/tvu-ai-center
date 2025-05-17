using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IEducationService
    {
        public Task CreateEducation(CreateEducationModel model, int userId);

        public Task UpdateEducation(int id, CreateEducationModel model);

        public Task<EducationDetailUpdateModel> FindDetailUpdateModel(int id);

        public Task<List<EducationModel>> GetAllEducation(string lang = "vi");

        public Task Delete(int id);

        public Task DeleteMultiple(List<int> ids);
    }
}
