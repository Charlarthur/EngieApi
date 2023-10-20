namespace EngieApi.Middleware
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate next;

        public RoutingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);

            if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Request.Path = "/index.html";
                await next(context);
            }
        }
    }
}
