using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Education_Categories")]
    public class EducationCategory
    {
        public int EducationId { get; set; }

        public int CategoryId { get; set; }
    }
}
