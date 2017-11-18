using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

public static class WatsonExtensions
{
    public static IApplicationBuilder UseWatson(this IApplicationBuilder app,WatsonOptions options)
    {
        return app.UseMiddleware<Watson>(Options.Create(options));
    }
}