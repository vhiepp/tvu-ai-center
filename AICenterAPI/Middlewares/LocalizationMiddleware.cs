using System.Globalization;

namespace AICenterAPI.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _supportedCultures = new[] { "vi", "en" };

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            var segments = path?.Split('/', StringSplitOptions.RemoveEmptyEntries);

            var lang = "vi";
            if (segments != null && segments.Length > 1)
            {
                lang = segments[1];
            }
            if (!_supportedCultures.Contains(lang))
            {
                lang = "vi";
            }
            var culture = new CultureInfo(lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }

    // Extension để gọi dễ dàng
    public static class LocalizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomLocalizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
