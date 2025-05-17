using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Education_Contents")]
    public class EducationContent
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Language { get; set; }

        public string? Slug { get; set; }

        public int EducationId { get; set; }
    }
}
