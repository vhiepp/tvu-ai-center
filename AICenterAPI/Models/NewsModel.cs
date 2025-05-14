namespace AICenterAPI.Models
{
    public class NewsModel
    {
    }

    public class NewsContentModel
    {
        public string? Title { get; set; }

        public string? Language { get; set; }

        public string? Content { get; set; } = string.Empty;

        public string? Slug { get; set; } = null;
    }

    public class CreateNewsModel
    {
        public List<int> Categories { get; set; } = new List<int>();

        public List<NewsContentModel> NewsContents { get; set; } = new List<NewsContentModel>();

        public string? Slug { get; set; } = null;

        public bool IsPublished { get; set; } = false;

        public IFormFile? Thumb { get; set; } = null;
    }
}
