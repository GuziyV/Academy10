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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
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
            //services.AddCors();
            var mapper = MapperConfiguration().CreateMapper();
            services.AddAutoMapper();
            /* services.AddCors();
             services.AddCors(options => {
                 options.AddPolicy("CorsPolicy",
                     builder => builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials());
             });*/

            services.AddDbContext<AirportContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AirportConnectionString"), b => b.MigrationsAssembly("Presentation Layer")));

           /* services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });
            */

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AirportContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc();


            AirportDbInitializer.Initialize(context).Wait();
        }
        
        public MapperConfiguration MapperConfiguration()
        {
            var config = MyMapperConfiguration.GetConfiguration();
            return config;
        }
    }
}