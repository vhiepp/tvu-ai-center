using AICenterAPI.Models;
using System.Text.Json;

namespace AICenterAPI.Services
{
    public class AboutService : IAboutService
    {
        private readonly string _filePath = "Storages/about.json";

        public async Task<AboutModel> About()
        {
            if (!File.Exists(_filePath))
            {
                return new AboutModel();
            }

            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<AboutModel>(json) ?? new AboutModel();
            return data;
        }

        public Task UpdateDescription(string description)
        {
            throw new NotImplementedException();
        }
    }
}
