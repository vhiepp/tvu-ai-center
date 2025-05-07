using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Page_Contents_Active")]
    public class PageContentActive
    {
        [Key]
        [Required]
        public string Key { get; set; } = string.Empty;

        public bool Status { get; set; } = true;
    }
}
