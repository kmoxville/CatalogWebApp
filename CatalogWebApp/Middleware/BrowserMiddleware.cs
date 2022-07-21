using Wangkanai.Detection.Services;

namespace CatalogWebApp.Middleware
{
    public class BrowserMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDetectionService detectionService)
        {
            if (detectionService.Browser.Name != Wangkanai.Detection.Models.Browser.Edge)
            {
                context.Response.Redirect("https://theuselessweb.site/nooooooooooooooo/");
            }

            await _next(context);
        }
    }

    public static class BrowserMiddlewareExtensions
    {
        public static void UseBrowserMiddleware(this WebApplication app)
        {
            app.UseMiddleware<BrowserMiddleware>();
        }
    }
}
