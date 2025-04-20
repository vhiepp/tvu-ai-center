using AICenterAPI.Extensions;
using AICenterAPI.Middlewares;

namespace AICenterAPI.Configurations
{
    public class MiddlewareConfig
    {
        public static void AddMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<PermissionMiddleware>();
            app.UseAppRequestLocalization();
            app.UseCustomLocalizationMiddleware();
        }
    }
}
