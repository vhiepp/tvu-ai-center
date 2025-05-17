using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface INewsService
    {
        public Task CreateNews(CreateNewsModel model, int userId);

        public Task UpdateNews(int id, CreateNewsModel model);

        public Task<NewsDetailUpdateModel> FindDetailUpdateModel(int id);

        public Task<List<NewsModel>> GetAllNews(string lang = "vi");

        public Task Delete(int id);

        public Task DeleteMultiple(List<int> ids);
    }
}
