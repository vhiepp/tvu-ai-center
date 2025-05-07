using AICenterAPI.Repositories;
using AICenterAPI.Services;

namespace AICenterAPI.Configurations
{
    public class RepositoryConfig
    {
        public static void AddRepositorys(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IWardRepository, WardRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryContentRepository, CategoryContentRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
            services.AddScoped<INewsContentRepository, NewsContentRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IPageContentRepository, PageContentRepository>();
            services.AddScoped<IPageContentActiveRepository, PageContentActiveRepository>();
            services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();

        }
    }
}
