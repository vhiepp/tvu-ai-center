using Slugify;

namespace AICenterAPI.Helpers
{
    public class SlugGenerator
    {
        private readonly SlugHelper _slugHelper;

        public SlugGenerator()
        {
            _slugHelper = new SlugHelper();
        }

        public string Make(string title)
        {
            string slug = _slugHelper.GenerateSlug(title);

            // Tạo số ngẫu nhiên 4 chữ số (có thể thay đổi độ dài)
            var random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return $"{slug}-{randomNumber}";
        }
    }
}
