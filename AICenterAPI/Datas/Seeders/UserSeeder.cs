using AICenterAPI.Services;

namespace AICenterAPI.Datas.Seeders
{
    public class UserSeeder
    {
        public static List<User> users = new List<User>
        {
            new User
            {
                UserName = "vanhiep",
                Password = "aicenter123",
                FirstName = "Văn Hiệp",
                IsSuperAdmin = true,
                IsCustomer = false,
            },
            new User
            {
                UserName = "nhutlam",
                Password = "aicenter123",
                FirstName = "Nhứt Lam",
                LastName = "Nguyễn",
                IsSuperAdmin = true,
                IsCustomer = false,
            },
        };

        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDBContext>();
                var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                // Đảm bảo cơ sở dữ liệu đã được tạo
                context.Database.EnsureCreated();

                foreach (var user in users)
                {
                    if (!context.Users.Any(p => p.UserName == user.UserName))
                    {
                        user.Password = authService.HashPassword(user.Password);
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
