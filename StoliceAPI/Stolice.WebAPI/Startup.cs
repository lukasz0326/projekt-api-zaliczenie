using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stolice.Database;
using Stolice.Services;
using Stolice.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stolice.WebAPI
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

            // rejestracja kontekstu baz danych
            services.AddDbContext<AppDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseSqlite(Configuration.GetConnectionString("sqlite"));

                // ponizszy zapis oznacza ze chcemy polaczyc sie z baza danych utworzona na SQL Server
                // gdzie jego connection string jest pobierany z pliku appsettings.json 

                // options.UseSqlServer(Configuration.GetConnectionString("stolice_db"));
            });

            // rejestracja moich serwisow
            services.AddScoped<ICapitalService, CapitalService>();
            services.AddScoped<ICountryService, CountryService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
