namespace AICenterAPI.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string? Avatar { get; set; } = string.Empty;

        public string? Fullname { get; set; } = string.Empty;

        public string? Position { get; set; } = string.Empty;

        public string? Skills { get; set; } = string.Empty;

        public DateTime? DateOfJoin { get; set; } = DateTime.Now;

        public string? Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? Facebook { get; set; } = null;

        public string? Tiktok { get; set; } = null;

        public string? Instagram { get; set; } = null;

        public string? Linkedln { get; set; } = null;

        public string? Github { get; set; } = null;

        public bool Pin { get; set; } = false;
    }

    public class CreateMemberModel
    {
        public IFormFile? Avatar { get; set; } = null;

        public string? Fullname { get; set; } = string.Empty;

        public string? Position { get; set; } = string.Empty;

        public string? Skills { get; set; } = string.Empty;

        public DateTime? DateOfJoin { get; set; } = DateTime.Now;

        public string? Email {  get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? Facebook { get; set; } = null;

        public string? Tiktok { get; set; } = null;

        public string? Instagram { get; set; } = null;

        public string? Linkedln { get; set; } = null;

        public string? Github { get; set; } = null;

        public bool Pin { get; set; } = false;
    }
}
