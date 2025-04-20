namespace AICenterAPI.Datas.Seeders
{
    public static class PermissionSeeder
    {
        public static List<Permission> permissions = new List<Permission>
        {
            new Permission
            {
                Name = "Quản lý trang Giới thiệu",
                KeyName = "AboutManager",
                Description = "Quản lý các thông tin trên trang Giới thiệu"
            },
            new Permission
            {
                Name = "Quản lý danh mục",
                KeyName = "CategoryManager",
                Description = "Quản lý các danh mục bao gồm thêm và sửa"
            },
            new Permission
            {
                Name = "Quản lý tin tức",
                KeyName = "NewsManager",
                Description = "Quản lý đăng tin tức bao gồm thêm, xóa, sửa"
            },
            new Permission
            {
                Name = "Quản lý sản phẩm",
                KeyName = "ProductManager",
                Description = "Quản lý đăng sản phẩm đã làm bao gồm thêm, xóa, sửa"
            },
            new Permission
            {
                Name = "Quản lý nghiên cứu",
                KeyName = "ResearchManager",
                Description = "Quản lý đăng tin các nghiên cứu đang thực hiện bao gồm thêm, xóa, sửa"
            },
            new Permission
            {
                Name = "Quản lý liên hệ",
                KeyName = "ContactManager",
                Description = "Quản lý thông tin liên hệ và các liên hệ của khách hàng"
            },
        };
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDBContext>();

                // Đảm bảo cơ sở dữ liệu đã được tạo
                context.Database.EnsureCreated();

                foreach (var permission in permissions)
                {
                    if (!context.Permissions.Any(p => p.KeyName == permission.KeyName))
                    {
                        context.Permissions.Add(permission);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
