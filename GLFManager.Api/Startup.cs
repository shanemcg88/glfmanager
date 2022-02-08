using FluentValidation.AspNetCore;
using GLFManager.Api.Helpers;
using GLFManager.App;
using GLFManager.App.Repositories;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.App.Services.JobServices;
using GLFManager.App.Services.UserServices;
using GLFManager.Middleware;
using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLFManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => 
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        
                    });
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                loggingBuilder.AddDebug();
            });

            

            // Set up database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly("GLFManager.App");
                });
            });

            // Add Identity Framework
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("Identity").GetValue<string>("Authority");
                    options.ApiName = "glfapi";
                    options.RequireHttpsMetadata = false;
                    
                });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                //s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                // FileProvider = new PhysicalFileProvider(
                //     Path.Combine(env.ContentRootPath, @"ClientApp/dist")),
                //     RequestPath = new PathString("/ClientApp/dist")
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
