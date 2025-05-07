namespace AICenterAPI.Models
{
    public class PageContentModel
    {
        public string Key { get; set; } = string.Empty;

        public string? Content { get; set; } = null;

        public string Language { get; set; } = string.Empty;
    }

    public class UpdatePageContentModel
    {
        public string? Content { get; set; } = null;

        public string Language { get; set; } = string.Empty;
    }
}
