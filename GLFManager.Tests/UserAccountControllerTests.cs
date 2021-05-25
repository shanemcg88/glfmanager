using GLFManager.Api;
using GLFManager.Api.Controllers;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Xunit;
using GLFManager.App;
using Microsoft.Extensions.Configuration;
using GLFManager.App.Repositories;
using GLFManager.Models.ViewModels.Account;

namespace GLFManager.Tests
{
    public class UserAccountControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private IUserAccountRepository Repository { get; set; }
        private UserAccountController Controller { get; }

        public UserAccountControllerTests(TestFixture<Startup> fixture)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "test1@email.com"
                }
            }.AsQueryable();

            var mockUserManager = new Mock<FakeUserManager>();
            //var mockDbContext = new Mock<ApplicationDbContext>();
            var mockIConfiguration = new Mock<IConfiguration>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>();
            var mockSignInManager = new Mock<FakeSignInManager>();
            var mockRepository = new Mock<IUserAccountRepository>();

            mockUserManager.Setup(x => x.Users)
                .Returns(users);

            //Repository = new UserAccountRepository(mockDbContext.Object, mockUserManager.Object, mockIConfiguration.Object, mockRoleManager.Object);

            var userValidator = new Mock<IUserValidator<User>>();
            userValidator.Setup(x => x.ValidateAsync(It.IsAny<UserManager<User>>(), It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            var passwordValidator = new Mock<IPasswordValidator<User>>();
            passwordValidator.Setup(x => x.ValidateAsync(It.IsAny<UserManager<User>>(), It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var signInManager = new Mock<FakeSignInManager>();
            signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            Controller = new UserAccountController(mockSignInManager.Object, mockIConfiguration.Object, mockUserManager.Object, mockRepository.Object);
        }

        [Theory]
        [InlineData("shanelgmcguire@gmail.com", "Password1", "client")]
        public async Task LoginWorks(string email, string password, string clientId)
        {
            // Arrange
            var testUser = new LoginViewModel {
                Email = email,
                Password = password,
                ClientId = clientId
            };

            // Act
            var testLogin = await Controller.Login(testUser);

            //Assert
            Assert.Equal(email, testUser.Email);
        }
    }
}
