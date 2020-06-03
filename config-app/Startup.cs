using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using config_app.DAL;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using config_app.Models;
using Microsoft.Identity.Client;

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
                options.UseMySql(Configuration.GetConnectionString("ConfigContext"));
            });
            services.AddScoped<IBeaconRepository, BeaconRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddProtectedWebApi(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new List<string>()
                    }
                });
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Config Api");
            });

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
                if (!context.Employees.Any())
                {
                    context.Employees.Add(new Employee { EmployeeId = "5347823982530", UserId = "e964c7ba-432f-4b56-ae9f-2f7b94e9f119" });
                    context.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
