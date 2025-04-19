using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("News_Categories")]
    public class NewsCategory
    {
        public int NewsId { get; set; }

        public int CategoryId { get; set; }
    }
}
