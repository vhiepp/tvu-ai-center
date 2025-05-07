using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("System_Config")]
    public class SystemConfig
    {
        [Key]
        [Required]
        public string Key { get; set; } = string.Empty;

        public string? Value { get; set; } = null;
    }
}
