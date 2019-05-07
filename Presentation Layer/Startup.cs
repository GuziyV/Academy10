using AutoMapper;
using BL.Helpers;
using BL.Services;
using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Contexts;
using Data_Access_Layer.DbInitializer;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.DTos;
using System.Text;

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
            services.AddScoped<UserService>();
			services.AddMvc();
			services.AddScoped<IMapper>(sp => MyMapperConfiguration.GetConfiguration());


			services.AddDbContext<AirportContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AirportConnectionString"), b => b.MigrationsAssembly("Presentation Layer")));


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x => {
		            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	            })
	            .AddJwtBearer(x => {
		            x.RequireHttpsMetadata = false;
		            x.SaveToken = true;
		            x.TokenValidationParameters = new TokenValidationParameters {
			            ValidateIssuerSigningKey = true,
			            IssuerSigningKey = new SymmetricSecurityKey(key),
			            ValidateIssuer = false,
			            ValidateAudience = false
		            };
	            });

            // Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
	            builder.WithOrigins("http://localhost:8080")
		            .AllowAnyMethod()
		            .AllowAnyHeader()
		            .AllowCredentials();
            }));

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AirportContext context)
        {
			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseMvc();


            AirportDbInitializer.Initialize(context).Wait();
        }
    }
}