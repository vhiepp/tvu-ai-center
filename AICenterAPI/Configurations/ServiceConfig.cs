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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IPageContentService, PageContentService>();
            services.AddScoped<IPageContentActiveService, PageContentActiveService>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<INewsService, NewsService>();
        }
    }
}
