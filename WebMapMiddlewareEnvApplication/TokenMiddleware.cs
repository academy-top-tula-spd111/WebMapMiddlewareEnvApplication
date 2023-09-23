namespace WebMapMiddlewareEnvApplication
{
    public class TokenMiddleware
    {
        readonly RequestDelegate _next;
        string pattern;

        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this._next = next;
            this.pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            var token = context.Request.Query["token"];
            if (token != pattern)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"{token} is not valid");
            }
            else
                await _next.Invoke(context);
        }
    }
}
