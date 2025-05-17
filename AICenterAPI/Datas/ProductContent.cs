using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Product_Contents")]
    public class ProductContent
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Language { get; set; }

        public string? Slug { get; set; }

        public int ProductId { get; set; }
    }
}
