using Microsoft.EntityFrameworkCore;
using UltraBusAPI.Datas;

namespace AICenterAPI.Configurations
{
    public class DatabaseConfig
    {
        public static void AddDbContext(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MyDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
