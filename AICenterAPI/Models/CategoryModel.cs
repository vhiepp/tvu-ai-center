namespace AICenterAPI.Models
{
    public class BaseCategoryModel
    {
        public string? Language { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }

    public class CategoryModel : BaseCategoryModel
    {
        public int Id { get; set; }

        public UserModel? Author { get; set; } = null;
    }

    public class CreateCategoryModel
    {
        public List<BaseCategoryModel> CategoryContents { get; set; } = new List<BaseCategoryModel>();
    }
}
