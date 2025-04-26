namespace AICenterAPI.Services
{
    public interface IUploadService
    {
        public Task<string?> SaveImage(IFormFile image);
    }
}
