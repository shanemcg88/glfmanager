using GLFManager.Api;
using GLFManager.Api.Controllers;
using GLFManager.App;
using GLFManager.App.Repositories;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.App.Seeds;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.Tests.IntegrationTests
{
    public class IntegrationTest<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    new IUserValidator<TIDentityUser>[0],
                    new IPasswordValidator<TIDentityUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }

        Mock<RoleManager<TIdentityRole>> GetRoleManagerMock<TIdentityRole>() where TIdentityRole : IdentityRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
                    new Mock<IRoleStore<TIdentityRole>>().Object,
                    new IRoleValidator<TIdentityRole>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }

        private IConfiguration _config;

        private ApplicationDbContext _context;

        public void UnitTestManager()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        public IConfiguration Configuration {
            get {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"F:/webdev/glfManager/GLFManager/GLFManager.Api/testsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(async services =>
            {

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<IntegrationTest<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {

                        
                        var user = new User { Email = "shanelgmcguire@gmail.com" };
                        var mockRoleManager = GetRoleManagerMock<IdentityRole>();
                        var mockUserManager = GetUserManagerMock<User>();
                        mockUserManager.Setup(s => s.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

                        await UserAndRoleSeeder.SeedUsersAndRoles(mockRoleManager.Object, mockUserManager.Object);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "an error occured seeding the database with UserAndRoleSeeder", ex.Message);
                    }
                }
            });
        }

        public UserAccountRepository InitializeRepository()
        {
            UnitTestManager();
            var mockRepository = new Mock<IUserAccountRepository>();
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockUserManager = GetUserManagerMock<User>();
            var mockRoleManager = GetRoleManagerMock<IdentityRole>();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql("Host=localhost;Port=33030;Database=glfTest;User Id=devdbuser;Password=devdbpassword");
            _context = new ApplicationDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
            var repository = new UserAccountRepository(mockDbContext.Object, mockUserManager.Object, _config, mockRoleManager.Object);

            return repository;
        }

        protected async Task AuthenticateAsync()
        {
            //TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        //private async Task<string> GetJwtAsync()
        //{
            
        //}

        //protected async Task LoginTest(LoginViewModel credentials)
        //{
        //    var response = await TestClient.PostAsync("/api/useraccount/login", new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json"));

        //    var message = await response.Content.ReadAsStringAsync();

        //    Console.Write(message);

        //}
    }
}
