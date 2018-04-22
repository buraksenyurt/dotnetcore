using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace SmartReaderApi.Middlewares
{
    public static class HttpOverriderExtensions
    {
        public static IApplicationBuilder UseHttpMethodOverriding(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<HttpOverrider>();
        }

        public static IApplicationBuilder UseHttpMethodOverriding(this IApplicationBuilder app, HttpOverriderOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<HttpOverrider>(Options.Create(options));
        }
    }
}