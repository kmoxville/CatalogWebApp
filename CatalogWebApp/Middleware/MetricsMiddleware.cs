using CatalogWebApp.Services.MetricsService;

namespace CatalogWebApp.Middleware
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate _next;

        public MetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMetricsService metricsService)
        {
            await metricsService.AddVisit(context.Request.Path);

            await _next(context);
        }
    }

    public static class MetricsMiddlewareExtensions
    {
        public static void UseMetricsMiddleware(this WebApplication app)
        {
            app.UseMiddleware<MetricsMiddleware>();
        }
    }
}
