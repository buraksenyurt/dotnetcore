using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceSensor.House
{
    public class Startup
    {
       public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddCors(o =>
            {
                o.AddPolicy("All", p =>
                {
                    p.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer();
            app.UseCors("All");

            app.UseSignalR(routes =>
            {
                routes.MapHub<HealthSensorHub>("/healthSensor");
            });
        }
    }
}