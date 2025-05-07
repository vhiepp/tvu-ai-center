using AICenterAPI.Datas.Seeders;

namespace AICenterAPI.Configurations
{
    public class SeederConfig
    {
        public static void Run(IServiceProvider service)
        {
            // Gọi Seeder
            // AddressSeeder.SeedData(service);
            PermissionSeeder.SeedData(service);
            UserSeeder.SeedData(service);
            PageContentSeeder.SeedData(service);
            CategorySeeder.SeedData(service);
        }
    }
}
