namespace AICenterAPI.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string? Slug { get; set; } = null;
        public string? Thumb { get; set; } = null;
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public NewsContentModel Content { get; set; } = new NewsContentModel();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public string? AuthorName { get; set; } = null;
        public string? AuthorAvatar { get; set; } = null;
        public string? AuthorSubtitle { get; set; } = null;
    }

    public class NewsDetailUpdateModel
    {
        public List<NewsContentModel> Contents { get; set; } = new List<NewsContentModel>();

        public List<int> CategoryIds { get; set; } = new List<int>();

        public string? Thumb { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

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
        public string? Categories { get; set; } = null;

        public string? NewsContents { get; set; } = null;

        public string? Slug { get; set; } = null;

        public bool IsPublished { get; set; } = false;

        public IFormFile? Thumb { get; set; } = null;
    }
}
