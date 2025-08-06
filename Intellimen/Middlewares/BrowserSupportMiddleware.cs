namespace Intellimen.Middlewares
{
    public class BrowserSupportMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserSupportMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"].ToString();

            if (context.Request.GetTypedHeaders().Accept.Any(header => header.MediaType == "text/html")
                && (!context.Request.Path.HasValue || !context.Request.Path.Value.Equals("/browsernotsupported"))
                && (userAgent.Contains("MSIE") || userAgent.Contains("Trident")))
            {
                context.Response.Redirect("/browsernotsupported");
                return;
            }

            await _next(context);
        }
    }
}
