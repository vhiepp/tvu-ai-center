
using System.Text.Json;

namespace AICenterAPI.Services
{
    public class UploadService : IUploadService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UploadService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string?> SaveImage(IFormFile image)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using (var form = new MultipartFormDataContent())
            {
                // Tạo StreamContent từ IFormFile
                var streamContent = new StreamContent(image.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);

                // Thêm file vào form data
                form.Add(streamContent, "image", image.FileName);

                // Gửi yêu cầu POST tới server NodeJS
                var response = await httpClient.PostAsync("http://localhost:3020/upload", form);

                // Nếu server NodeJS trả về kết quả thành công
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    using var document = JsonDocument.Parse(result);
                    var fileUrl = document.RootElement.GetProperty("fileUrl").GetString();
                    return fileUrl;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
