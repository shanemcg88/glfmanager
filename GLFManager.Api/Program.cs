using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GLFManager.Models.Entities;
using GLFManager.App;
using Microsoft.EntityFrameworkCore;
using GLFManager.App.Seeds;

namespace GLFManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                    Task.Run(async () => await UserAndRoleSeeder.SeedUsersAndRoles(roleManager, userManager)).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error has occured while migrating to the database.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
