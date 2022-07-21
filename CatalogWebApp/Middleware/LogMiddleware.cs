namespace CatalogWebApp.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LogMiddleware> logger)
        {
            Stream originalBody = context.Response.Body;
            context.Request.EnableBuffering();

            using (var memStream = new MemoryStream())
            {
                await context.Request.Body.CopyToAsync(memStream);
                memStream.Position = 0;
                using (var streamReader = new StreamReader(memStream))
                {
                    string text = await streamReader.ReadToEndAsync();
                    logger.LogInformation("Request body: " + text);
                }
                
                context.Request.Body.Position = 0;
            }

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _next(context);
                    memStream.Position = 0;
                    string responseBody = await new StreamReader(memStream).ReadToEndAsync();
                    logger.LogInformation("Response body: " + responseBody);
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static void UseLogMiddleware(this WebApplication app)
        {
            app.UseMiddleware<LogMiddleware>();
        }
    }
}
