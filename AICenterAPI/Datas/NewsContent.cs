using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("News_Contents")]
    public class NewsContent
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Language { get; set; }

        public string? Slug { get; set; }

        public int NewsId { get; set; }
    }
}
