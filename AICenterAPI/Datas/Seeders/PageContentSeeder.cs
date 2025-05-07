namespace AICenterAPI.Datas.Seeders
{
    public static class PageContentSeeder
    {
        public static List<PageContent> pageContents = new List<PageContent>
        {
            new PageContent
            {
                Key = "about",
                Language = "en",
                Content = ""
            },
            new PageContent
            {
                Key = "about",
                Language = "vi",
                Content = ""
            },
        };
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDBContext>();

                // Đảm bảo cơ sở dữ liệu đã được tạo
                context.Database.EnsureCreated();

                foreach (var pageContent in pageContents)
                {
                    if (!context.PageContents.Any(p => p.Key == pageContent.Key && p.Language == pageContent.Language))
                    {
                        context.PageContents.Add(pageContent);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
