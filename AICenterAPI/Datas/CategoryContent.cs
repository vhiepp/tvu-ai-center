using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Category_Contents")]
    public class CategoryContent
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Language { get; set; }

        public int CategoryId { get; set; }
    }
}
