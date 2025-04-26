using System.ComponentModel.DataAnnotations;

namespace AICenterAPI.Datas
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; } = null;

        public string? Logo { get; set; } = null;

        public Status Status { get; set; } = Status.Show;

        public int? NumberOrder { get; set; } = null;
    }
}
