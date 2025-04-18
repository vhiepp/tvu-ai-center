namespace AICenterAPI.Models
{
    public class AboutModel
    {
        public string Description { get; set; } = String.Empty;

        public List<PartnerModel> Partners { get; set; } = new List<PartnerModel>();

        public FunctionalTaskModel FunctionalTask { get; set; } = new FunctionalTaskModel();

        public List<TimelineModel> Timelines { get; set; } = new List<TimelineModel>();
    }

    public class PartnerModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;
    }

    public class FunctionalTaskModel
    {
        public string Title { get; set; } = string.Empty;

        public List<string> Lists { get; set; } = new List<string>();
    }

    public class TimelineModel
    {
        public string SubTitle {  get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
