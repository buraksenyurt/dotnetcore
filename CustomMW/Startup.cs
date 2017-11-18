using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomMW
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Use(async (HttpContext ctx,Func<Task> next)=>{
            //     var request=ctx.Request;
            //     Console.WriteLine("REQUEST {0}",DateTime.Now.ToLongTimeString());
            //     Console.WriteLine("{0}-{1}{2}",request.Method,request.Host,request.Path);

            //     await next.Invoke();

            //     var response=ctx.Response;
            //     Console.WriteLine("RESPONSE:{0}",DateTime.Now.ToLongTimeString());
            //     Console.WriteLine("{0}\t({1}){2}",response.ContentType,response.StatusCode, (HttpStatusCode)response.StatusCode);
            // });
            app.UseWatson(new WatsonOptions{
                MaxSizeForPostContent=1024
            });

            app.UseMvc();

        }
    }
}
