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
