namespace AICenterAPI.Datas.Seeders
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public List<CategoryContent> CategoryContents { get; set; } = new List<CategoryContent>();
    }
    public static class CategorySeeder
    {
        public static List<CategoryModel> categories = new List<CategoryModel>
{
            new CategoryModel
            {
                Id = 1,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 1, Language = "vi", Name = "Học máy", Description = "Các bài viết về thuật toán và ứng dụng của học máy trong thực tế." },
                    new CategoryContent { CategoryId = 1, Language = "en", Name = "Machine Learning", Description = "Articles about algorithms and real-world applications of machine learning." }
                }
            },
            new CategoryModel
            {
                Id = 2,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 2, Language = "vi", Name = "Học sâu", Description = "Khám phá các mạng nơ-ron và mô hình học sâu tiên tiến." },
                    new CategoryContent { CategoryId = 2, Language = "en", Name = "Deep Learning", Description = "Exploring neural networks and advanced deep learning models." }
                }
            },
            new CategoryModel
            {
                Id = 3,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 3, Language = "vi", Name = "Xử lý ngôn ngữ tự nhiên", Description = "Nội dung về chatbot, phân tích ngôn ngữ và mô hình NLP." },
                    new CategoryContent { CategoryId = 3, Language = "en", Name = "Natural Language Processing", Description = "Content on chatbots, language analysis, and NLP models." }
                }
            },
            new CategoryModel
            {
                Id = 4,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 4, Language = "vi", Name = "Thị giác máy tính", Description = "Ứng dụng nhận diện hình ảnh, video và xử lý hình ảnh số." },
                    new CategoryContent { CategoryId = 4, Language = "en", Name = "Computer Vision", Description = "Applications in image recognition, video analysis, and digital image processing." }
                }
            },
            new CategoryModel
            {
                Id = 5,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 5, Language = "vi", Name = "Robot và tự động hóa", Description = "Tin tức và kiến thức về robot, tự động hóa thông minh." },
                    new CategoryContent { CategoryId = 5, Language = "en", Name = "Robotics & Automation", Description = "News and insights on robotics and smart automation." }
                }
            },
            new CategoryModel
            {
                Id = 6,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 6, Language = "vi", Name = "Ứng dụng AI", Description = "AI được ứng dụng trong đời sống, công việc và công nghiệp." },
                    new CategoryContent { CategoryId = 6, Language = "en", Name = "AI Applications", Description = "AI applications in life, work, and industry." }
                }
            },
            new CategoryModel
            {
                Id = 7,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 7, Language = "vi", Name = "Phân tích dữ liệu", Description = "Kỹ thuật phân tích và trực quan hóa dữ liệu với AI." },
                    new CategoryContent { CategoryId = 7, Language = "en", Name = "Data Analytics", Description = "Techniques for data analysis and visualization with AI." }
                }
            },
            new CategoryModel
            {
                Id = 8,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 8, Language = "vi", Name = "Thuật toán AI", Description = "Tổng hợp các thuật toán phổ biến trong trí tuệ nhân tạo." },
                    new CategoryContent { CategoryId = 8, Language = "en", Name = "AI Algorithms", Description = "Overview of common algorithms in artificial intelligence." }
                }
            },
            new CategoryModel
            {
                Id = 9,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 9, Language = "vi", Name = "Đạo đức và pháp lý AI", Description = "Thảo luận về các vấn đề đạo đức và pháp luật liên quan đến AI." },
                    new CategoryContent { CategoryId = 9, Language = "en", Name = "AI Ethics & Legal", Description = "Discussion on ethical and legal issues surrounding AI." }
                }
            },
            new CategoryModel
            {
                Id = 10,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 10, Language = "vi", Name = "AI trong đời sống", Description = "Cách AI đang thay đổi cuộc sống hằng ngày của chúng ta." },
                    new CategoryContent { CategoryId = 10, Language = "en", Name = "AI in Daily Life", Description = "How AI is transforming our daily lives." }
                }
            },
            new CategoryModel
            {
                Id = 11,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 11, Language = "vi", Name = "AI trong y tế", Description = "Ứng dụng AI trong chẩn đoán, điều trị và chăm sóc sức khỏe." },
                    new CategoryContent { CategoryId = 11, Language = "en", Name = "AI in Healthcare", Description = "AI applications in diagnosis, treatment, and healthcare." }
                }
            },
            new CategoryModel
            {
                Id = 12,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 12, Language = "vi", Name = "AI trong kinh doanh", Description = "AI hỗ trợ tối ưu hóa quy trình và ra quyết định trong doanh nghiệp." },
                    new CategoryContent { CategoryId = 12, Language = "en", Name = "AI in Business", Description = "AI for optimizing processes and decision-making in businesses." }
                }
            },
            new CategoryModel
            {
                Id = 13,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 13, Language = "vi", Name = "AI mã nguồn mở", Description = "Tổng hợp các dự án và công cụ AI mã nguồn mở." },
                    new CategoryContent { CategoryId = 13, Language = "en", Name = "Open Source AI", Description = "A collection of open-source AI projects and tools." }
                }
            },
            new CategoryModel
            {
                Id = 14,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 14, Language = "vi", Name = "Tài nguyên học AI", Description = "Tài liệu, khóa học và nguồn học AI cho người mới bắt đầu lẫn nâng cao." },
                    new CategoryContent { CategoryId = 14, Language = "en", Name = "AI Learning Resources", Description = "Materials, courses, and learning resources for beginners and advanced learners." }
                }
            },
            new CategoryModel
            {
                Id = 15,
                CategoryContents = new List<CategoryContent>
                {
                    new CategoryContent { CategoryId = 15, Language = "vi", Name = "Tin tức AI", Description = "Cập nhật tin tức mới nhất về trí tuệ nhân tạo trong và ngoài nước." },
                    new CategoryContent { CategoryId = 15, Language = "en", Name = "AI News", Description = "Latest updates on artificial intelligence worldwide." }
                }
            }
        };



        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDBContext>();

                // Đảm bảo cơ sở dữ liệu đã được tạo
                context.Database.EnsureCreated();

                foreach (var category in categories)
                {
                    if (!context.Categories.Any(p => p.Id == category.Id))
                    {
                        var newCategory = new Category { Id = category.Id, AuthorId = 1};
                        context.Categories.Add(newCategory);
                        foreach (var content in category.CategoryContents)
                        {
                            var newContent = new CategoryContent
                            {
                                CategoryId = newCategory.Id,
                                Language = content.Language,
                                Name = content.Name,
                                Description = content.Description
                            };
                            context.CategoryContents.Add(newContent);
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
