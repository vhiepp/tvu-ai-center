using AICenterAPI.Services;

namespace AICenterAPI.Configurations
{
    public class ServiceConfig
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAboutService, AboutService>();
        }
    }
}
