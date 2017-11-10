using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloJWT
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
            
            services.AddAuthentication(options=>{
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options=>{
                options.TokenValidationParameters=new TokenValidationParameters{
                    ValidateAudience=true,                                        
                    ValidAudience="heimdall.fabrikam.com",
                    ValidateIssuer=true,
                    ValidIssuer="west-world.fabrikam.com",
                    ValidateLifetime=true,
                    //ClockSkew=TimeSpan.Zero,
                    ValidateIssuerSigningKey=true,                    
                    IssuerSigningKey=new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"))
                };

                options.Events=new JwtBearerEvents{
                    OnTokenValidated=ctx=>{                        
                        //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulam yapılabilir.
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed=ctx=>{
                        Console.WriteLine("Exception:{0}",ctx.Exception.Message);
                        return Task.CompletedTask;
                    }                  
                };
            });     
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
