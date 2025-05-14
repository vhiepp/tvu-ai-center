using System.ComponentModel.DataAnnotations;

namespace AICenterAPI.Datas
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        public string? Slug { get; set; }

        public string? Thumb { get; set; }

        public string? AuthorAvatar { get; set; }

        public string? AuthorName { get; set; }

        public string? AuthorSubtitle { get; set; }

        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime PublishedAt { get; set; }

        public bool IsPublished { get; set; } = false;
    }
}
