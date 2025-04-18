using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AICenterAPI.Datas
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string KeyName { get; set; } = string.Empty;

        [AllowNull]
        public string? Description { get; set; }

    }
}
