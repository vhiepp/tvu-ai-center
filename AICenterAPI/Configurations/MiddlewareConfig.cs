using AICenterAPI.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace AICenterAPI.Configurations
{
    public class MiddlewareConfig
    {
        public static void AddMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<PermissionMiddleware>();
        }
    }
}
