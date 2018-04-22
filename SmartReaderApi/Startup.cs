using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartReaderApi.Middlewares;
using Microsoft.AspNetCore.Http;

namespace SmartReaderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<HttpOverrider>(Options.Create(new HttpOverriderOptions
            {
                HeaderName = "X-HTTP-Method-Override",
                AllowedMethods = new[] { 
                    HttpMethods.Put
                    ,HttpMethods.Post
                    , HttpMethods.Delete
                    , HttpMethods.Get
                    }
            }));

            app.UseMvc();
        }
    }
}
