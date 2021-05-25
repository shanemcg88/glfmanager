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


namespace GLFManager.Tests
{
    public class UserAccountRepositoryTests
    {

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
        


        private Mock<SignInManager<User>> GetMockSignInManager(Mock userManager)
        {
            //var mockUserManager = GetMockUserManager();
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockClaimsPrincipal = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var mockOptions = new Mock<IOptions<IdentityOptions>>().Object;
            var mockLogger = new Mock<ILogger<SignInManager<User>>>().Object;
            var mockAuthScheme = new Mock<IAuthenticationSchemeProvider>().Object;
            var mockUserConfirmation = new Mock<IUserConfirmation<User>>().Object;
            return new Mock<SignInManager<User>>(userManager.Object, mockContextAccessor, mockClaimsPrincipal, mockOptions, mockLogger, mockAuthScheme, mockUserConfirmation);
        }
        [Fact]
        public async Task GetUserByEmailInLoginProcess()
        {
            // Arrange
            var loginCredentials = new LoginViewModel() { Email = "shanelgmcguire@gmail.com", Password = "Password1", ClientId = "client" };
            var mockUserAccountRepo = new Mock<IUserAccountRepository>();

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);

            mockUserAccountRepo.Setup(repo => repo.GetUserByEmail(loginCredentials.Email))
                .ReturnsAsync(new User { Email = loginCredentials.Email, UserName = loginCredentials.Email });
            var controller = new UserAccountController(mockSignInManager.Object, mockConfiguration.Object, mockUserManager.Object, mockUserAccountRepo.Object);

            // Act
            var result = await controller.Login(loginCredentials);

            // Assert
            Assert.NotNull(result);
        }   
    }
}
