using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using config_app.DAL;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace config_app
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
            services.AddDbContext<ConfigAppContext>(options =>
            {
                //TODO: Set this
                options.UseMySql(Configuration.GetConnectionString("ConfigContext"));
            });
            services.AddScoped<IBeaconRepository, BeaconRepository>();
            // Database opzetten, testen
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<ConfigAppContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (!context.BeaconMappings.Any())
                {
                    context.BeaconMappings.Add(new BeaconMapping
                    {
                        ConnectableName = "HMSoft",
                        ProximityUuid = "644f76f7-6a52-42bc-e911-fd902c9bb987",
                        Identifier = "",
                        Major = 1,
                        Minor = 1,
                        ReadableName = "Huis van Soufian Tichattibin"
                    });
                    context.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
