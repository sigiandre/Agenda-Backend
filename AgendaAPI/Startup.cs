using System;
using AgendaAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AgendaAPI
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Agenda API",
                    Description = "A simple example ASP.NET Core Web API - Agenda",
                    TermsOfService = new Uri("http://andrewprogramador.wixsite.com/cibernet"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sigi Andre Diaz Quiroz",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/SigiAndreDiaz"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Enlace GitHub",
                        Url = new Uri("https://github.com/sigiandre/Agenda-Backend"),
                    }
                });
            });

            services.AddDbContext<DonationDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgendaAPI V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
