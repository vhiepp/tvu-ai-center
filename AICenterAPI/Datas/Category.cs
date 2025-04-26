using System.ComponentModel.DataAnnotations;

namespace AICenterAPI.Datas
{
    public class Category
    {             
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }
    }
}
