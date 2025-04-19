using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IAboutService
    {
        public Task<AboutModel> About();

        public Task UpdateDescription(string description);
    }
}
