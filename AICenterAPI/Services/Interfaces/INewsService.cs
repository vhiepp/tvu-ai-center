using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface INewsService
    {
        public Task CreateNews(CreateNewsModel model, int userId);
    }
}
