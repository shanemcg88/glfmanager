using Microsoft.Extensions.Configuration;
using GLFManager.Api.Controllers;
using GLFManager.App;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using GLFManager.Models.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using GLFManager.App.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static GLFManager.App.ApplicationDbContext;

namespace GLFManager.Tests
{
    public class UserAccountRepositoryTests
    {
        private readonly ApplicationDbContext _context;

        public UserAccountRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql("Host=localhost;Port=33030;Database=glfTest;User Id=devdbuser;Password=devdbpassword");
            _context = new ApplicationDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

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

        [Fact]
        public async Task GetUserByEmailInLoginProcess()
        {
            // Arrange
            var getEmail = "test1@email.com";
            var mockRepository = new Mock<IUserAccountRepository>();
            mockRepository.Setup(repo => repo.GetUserByEmail(getEmail))
                .ReturnsAsync(new User { Id = "1", Email = "test1@email.com" });

            var mockUserManager = GetUserManagerMock<User>();
            var mockRoleManager = GetRoleManagerMock<IdentityRole>();
            var mockConfiguration = new Mock<IConfiguration>();

            var repository = new UserAccountRepository(_context, mockUserManager.Object, mockConfiguration.Object, mockRoleManager.Object);

            // Act
            var result = await repository.GetUserByEmail("shanelgmcguire@gmail.com");
            Console.WriteLine("!!!!!!!!!RESULT " + result);
            // Assert
            Assert.NotNull(result);

            TearDown();
        }

        public List<User> FakeUserData()
        {
            List<User> userData = new List<User>();
            userData.Add(new User { Id = "1", Email = "test1@email.com" });
            userData.Add(new User { Id = "2", Email = "test2@email.com" });

            return userData;
        }

        //private Mock<UserManager<User>> GetMockUserManager()
        //{
        //    var userStoreMock = new Mock<IUserStore<User>>();
        //    return new Mock<UserManager<User>>(
        //        userStoreMock.Object, null, null, null, null, null, null, null, null);
        //}

        //private Mock<SignInManager<User>> GetMockSignInManager(Mock userManager)
        //{
        //    //var mockUserManager = GetMockUserManager();
        //    var mockContextAccessor = new Mock<IHttpContextAccessor>();
        //    var mockClaimsPrincipal = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
        //    var mockOptions = new Mock<IOptions<IdentityOptions>>().Object;
        //    var mockLogger = new Mock<ILogger<SignInManager<User>>>().Object;
        //    var mockAuthScheme = new Mock<IAuthenticationSchemeProvider>().Object;
        //    var mockUserConfirmation = new Mock<IUserConfirmation<User>>().Object;
        //    return new Mock<SignInManager<User>>(userManager.Object, mockContextAccessor, mockClaimsPrincipal, mockOptions, mockLogger, mockAuthScheme, mockUserConfirmation);
        //}
        //[Fact]
        //public async Task GetUserByEmailInLoginProcess()
        //{
        //    // Arrange
        //    var loginCredentials = new LoginViewModel() { Email = "shanelgmcguire@gmail.com", Password = "Password1", ClientId = "client" };
        //    var mockUserAccountRepo = new Mock<IUserAccountRepository>();

        //    var contextAccessor = new Mock<IHttpContextAccessor>();
        //    var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    var mockUserManager = GetMockUserManager();
        //    var mockSignInManager = GetMockSignInManager(mockUserManager);

        //    mockUserAccountRepo.Setup(repo => repo.GetUserByEmail(loginCredentials.Email))
        //        .ReturnsAsync(new User { Email = loginCredentials.Email, UserName = loginCredentials.Email });
        //    var controller = new UserAccountController(mockSignInManager.Object, mockConfiguration.Object, mockUserManager.Object, mockUserAccountRepo.Object);

        //    // Act
        //    var result = await controller.Login(loginCredentials);

        //    // Assert
        //    Assert.NotNull(result);
        //}   
    }
}
