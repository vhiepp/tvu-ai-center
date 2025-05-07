using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Page_Contents")]
    public class PageContent
    {
        public string Key { get; set; } = string.Empty;

        public string? Content { get; set; } = null;

        public string Language { get; set; } = string.Empty;
    }
}
