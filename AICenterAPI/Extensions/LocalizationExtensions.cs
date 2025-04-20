using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace AICenterAPI.Extensions
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddAppLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }

        public static IApplicationBuilder UseAppRequestLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
            new CultureInfo("vi"),
            new CultureInfo("en")
        };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            return app.UseRequestLocalization(options);
        }
    }
}
