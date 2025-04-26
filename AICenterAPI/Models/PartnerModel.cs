using AICenterAPI.Datas;

namespace AICenterAPI.Models
{
    public class PartnerViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Logo { get; set; } = string.Empty;

        public Status Status { get; set; }

    }

    public class CreatePartnerModel
    {
        public string? Name { get; set; }

        public IFormFile? Logo { get; set; }

        public Status Status { get; set; } = Status.Show;
    }
}
