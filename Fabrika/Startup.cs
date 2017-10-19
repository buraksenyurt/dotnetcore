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
using Microsoft.EntityFrameworkCore;
using Fabrika.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace Fabrika
{
    public class Startup
    {
		/// <summary>
		///
		/// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

		/// <summary>
		///
		/// </summary>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<FabrikaContext>(opt=>opt.UseInMemoryDatabase("FabrikaDb"));
            services.AddMvc();
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { 
					Title = "Fabrika API"
					, Version = "v1" 
					, Description ="Fabrika'da üretilen ürünler hakkında bilgiler"
					, Contact=new Contact{Name="Burak Selim Şenyurt", Email="", Url="http://www.buraksenyurt.com"},
					License=new License{Name="Under GNU", Url="http://www.buraksenyurt.com"}
					});
					
					var basePath=PlatformServices.Default.Application.ApplicationBasePath;
					var xmlPath=Path.Combine(basePath,"Fabrika.xml");
					c.IncludeXmlComments(xmlPath);
			});
			
        }

		/// <summary>
		///
		/// </summary>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseStaticFiles();
            app.UseMvc();
			
			app.UseSwagger();
			app.UseSwaggerUI(c=>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json","Fabrika API v1.0");
			});
        }
    }
}
