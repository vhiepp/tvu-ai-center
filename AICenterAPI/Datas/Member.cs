using System.ComponentModel.DataAnnotations;

namespace AICenterAPI.Datas
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string? Fullname { get; set; } = null;

        public string? Avatar { get; set; } = null;

        public string? Position {  get; set; } = null;

        public string? Skills { get; set; } = null;

        public DateTime? DateOfJoin { get; set; } = DateTime.Now;

        public string? Email {  get; set; } = null;

        public string? PhoneNumber { get; set; } = null;

        public string? Description { get; set; } = null;

        public string? Facebook { get; set; } = null;

        public string? Tiktok { get; set; } = null;

        public string? Instagram { get; set; } = null;

        public string? Linkedln { get; set; } = null;

        public string? Github { get; set; } = null;

        public int? NumberOrder { get; set; } = null;

        public Status Status { get; set; } = Status.Show;

        public bool Pin {  get; set; } = false;
    }
}
