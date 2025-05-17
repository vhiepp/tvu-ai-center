namespace AICenterAPI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Slug { get; set; } = null;
        public string? Thumb { get; set; } = null;
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public string? Link { get; set; } = null;
        public ProductContentModel Content { get; set; } = new ProductContentModel();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public string? AuthorName { get; set; } = null;
        public string? AuthorAvatar { get; set; } = null;
        public string? AuthorSubtitle { get; set; } = null;
    }

    public class ProductDetailUpdateModel
    {
        public List<ProductContentModel> Contents { get; set; } = new List<ProductContentModel>();

        public List<int> CategoryIds { get; set; } = new List<int>();

        public string? Thumb { get; set; } = string.Empty;

        public string? Link { get; set; } = null;

        public bool IsPublished { get; set; } = false;

    }

    public class ProductContentModel
    {
        public string? Title { get; set; }

        public string? Language { get; set; }

        public string? Content { get; set; } = string.Empty;

        public string? Slug { get; set; } = null;
    }

    public class CreateProductModel
    {
        public string? Categories { get; set; } = null;

        public string? ProductContents { get; set; } = null;

        public string? Slug { get; set; } = null;

        public string? Link { get; set; } = null;

        public bool IsPublished { get; set; } = false;

        public IFormFile? Thumb { get; set; } = null;
    }
}
