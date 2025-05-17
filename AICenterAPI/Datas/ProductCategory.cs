using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Product_Categories")]
    public class ProductCategory
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
    }
}
