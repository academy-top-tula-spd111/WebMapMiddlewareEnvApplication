namespace WebMapMiddlewareEnvApplication
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseMyToken(
                this IApplicationBuilder app, string pattern)
        {
            return app.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}
