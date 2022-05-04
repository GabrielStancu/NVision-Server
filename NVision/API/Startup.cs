using API.Security;
using Core.Contexts;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace API
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
            // AutoMapper 
            services.AddAutoMapper(typeof(DtoMappingModule));

            // Enable CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Contexts 
            services.AddDbContext<NVisionDbContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("NVisionConnStr")));

            // SignalR
            services.AddSignalR();

            // Repositories 
            services.AddRepositories();

            // App Services 
            services.AddServices();

            // App Helpers
            services.AddHelpers();

            // JWT
            services.AddJwt(
                ConfigurationManager.GetIssuer(),
                ConfigurationManager.GetAudience(),
                ConfigurationManager.GetKey());


            // Controllers
            services.AddControllers().AddNewtonsoftJson();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NVision API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NVision API");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ProfilePictures")),
                RequestPath = "/ProfilePictures"
            });
        }
    }
}
