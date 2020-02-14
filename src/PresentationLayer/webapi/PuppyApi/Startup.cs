using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PuppyApi.Data;
using PuppyApi.Managers;
using System;
using System.IO;

namespace PuppyApi
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
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddMvcOptions(o => o.EnableEndpointRouting = false);

            services.AddSingleton(typeof(IExceptionHandler), typeof(ExceptionHandler));
            services.AddSingleton(typeof(IPottyBreakRepository), typeof(PottyBreakRepository));
            services.AddControllers();
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               );
                               
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "PuppyTracker/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}"
                    ); ;
            });

            app.UseSpa(spa => 
            {
                spa.Options.SourcePath = "PuppyTracker";

                if(env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
