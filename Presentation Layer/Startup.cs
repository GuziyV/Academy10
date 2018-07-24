using AutoMapper;
using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Contexts;
using Data_Access_Layer.DbInitializer;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs;

namespace Presentation_Layer
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
            services.AddScoped<IUnitOfWork, AirportUnitOfWork>();
            services.AddScoped<AirportService>();
            services.AddMvc();
            var mapper = MapperConfiguration().CreateMapper();
            services.AddAutoMapper();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<AirportContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AirportConnectionString"), b => b.MigrationsAssembly("Presentation Layer")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AirportContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            AirportDbInitializer.Initialize(context).Wait();
        }

        public MapperConfiguration MapperConfiguration()
        {
            var config = MyMapperConfiguration.GetConfiguration();
            return config;
        }
    }
}
