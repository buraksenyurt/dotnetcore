using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System;
using Microsoft.AspNetCore.Routing.Template;

namespace RouterSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseUrls("http://localhost:4001")
            .UseStartup<BoosterV3>()
            .Build();

            host.Run();
        }
    }

    class Booster
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var rootBuilder = new RouteBuilder(app);

            rootBuilder.MapGet("", (context) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                return context.Response.WriteAsync($"<h1><p style='color:orange'>Hoşgeldin Sahip</p></h1><i>Bugün nasılsın?</i>");
            }
            );

            rootBuilder.MapGet("green/mile", (context) =>
            {
                var routeData = context.GetRouteData();
                context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                return context.Response.WriteAsync($"Vayyy <b>Gizli yolu</b> buldun!<br/>Tebrikler.");
            }
            );

            /*rootBuilder.MapGet("{*urlPath}", (context) =>
            {
                var routeData = context.GetRouteData();
                return context.Response.WriteAsync($"Path bilgisi : {string.Join(",", routeData.Values)}");
            }
            );*/
            rootBuilder.MapGet("whatyouwant/{wanted=1 Bitcoin please}", (context) =>
            {
                var values = context.GetRouteData().Values;
                context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                return context.Response.WriteAsync($"İstediğin şey bu.<h2>{values["wanted"]}</h2>OLDU BİL :)");
            });

            app.UseRouter(rootBuilder.Build());
        }
    }

    class BoosterV2
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var handler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                var path = context.Request.Path;
                if (path == "/products/books")
                {
                    context.Response.Headers.Add("Content-Type", "application/json");
                    var books = File.ReadAllText("books.json");
                    return context.Response.WriteAsync(books);
                }

                context.Response.Headers.Add("Content-Type", "text/html;charset=utf-8");
                return context.Response.WriteAsync(
                    $@" 
                    <html>
					<body>
                    <h2>Selam Patron! Bugün nasılsın?</h2>
					{DateTime.Now.ToString()}
                    <ul>
                        <li><a href='/products/books'>Senin için bir kaç kitabım var. Haydi tıkla.</a></li>
						<li><a href='https://github.com/buraksenyurt'>Bu ve diğer .Net Core örneklerine bakmak istersen Git!</a></li>
                    </ul>                     
                    </body>
					</html>
                    ");
            });
            app.UseRouter(handler);
        }
    }

    class BoosterV3
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var apiSegment = new TemplateSegment();
            apiSegment.Parts.Add(TemplatePart.CreateLiteral("api"));

            var serviceNameSegment = new TemplateSegment();
            serviceNameSegment.Parts.Add(
                TemplatePart.CreateParameter("serviceName",
                    isCatchAll: false,
                    isOptional: true,
                    defaultValue: null,
                    inlineConstraints: new InlineConstraint[] { })
            );

            var segments = new TemplateSegment[] {
                apiSegment,
                serviceNameSegment
            };

            var routeTemplate = new RouteTemplate("default", segments.ToList());
            var templateMatcher = new TemplateMatcher(routeTemplate, new RouteValueDictionary());

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-type", "text/html");
                var requestPath = context.Request.Path;
                var routeData = new RouteValueDictionary();
                var isMatch = templateMatcher.TryMatch(requestPath, routeData);
                await context.Response.WriteAsync($"Request Path is <i>{requestPath}</i><br/>Match state is <b>{isMatch}</b><br/>Requested service name is {routeData["serviceName"]}");
                await next.Invoke();
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("");
            });
        }
    }
}