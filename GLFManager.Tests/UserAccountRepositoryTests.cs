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



        //private Mock<SignInManager<User>> GetMockSignInManager()
        //{
        //    var signInMock = new Mock<IUserStore<User>>();
        //    return new Mock<SignInManager<User>>(signInMock.Object, null, null);

        //}

        private Mock<SignInManager<User>> GetMockSignInManager(Mock userManager)
        {
            //var mockUserManager = GetMockUserManager();
            var mockContextAccessor = new Mock<IHttpContextAccessor>().Object;
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
        //[Fact]
        //public void GetUserByEmailTest()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(databaseName: "UserDatabase")
        //        .Options;
        //    var user1 = new User { Id = "1", Email = "test1@email.com", UserName = "test1@email.com" };
        //    var user2 = new User { Id = "2", Email = "test2@email.com", UserName = "test2@email.com" };

        //    using (var context = new ApplicationDbContext(options))
        //    {

        //        context.Users.Add(user1);
        //        context.Users.Add(user2);
        //        context.SaveChanges();
        //    }

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        var userAccountRepository = new Mock<IUserAccountRepository>();
        //        userAccountRepository.Setup(repo => repo.GetUserByEmail("something"))
        //            .ReturnsAsync(user1);

        //        var controller = new UserAccountController(userAccountRepository.Object);
        //    }
        //}
        //[Fact]
        //public async Task GetUserWithEmail()
        //{
        //    string userEmail = "test@email.com";
        //    var userAccountRepository = new Mock<IUserAccountRepository>();
        //    var newUser = new User() { Id = "94ee0b6b-3410-4674-9cd4-a986ed3cbd85", Email = userEmail, UserName = userEmail };

        //    userAccountRepository.Setup(repo => repo.GetUserByEmail(userEmail))
        //        //.ReturnsAsync(new User { Id = "94ee0b6b-3410-4674-9cd4-a986ed3cbd85", Email = userEmail, UserName = userEmail });
        //        .ReturnsAsync(new User { Id = "94ee0b6b-3410-4674-9cd4-a986ed3cbd85", Email = userEmail, UserName = userEmail });


        //}

        //private List<User> UserData()
        //{
        //    var userList = new List<User>();
        //    userList.Add(new User { Id = "94ee0b6b-3410-4674-9cd4-a986ed3cbd85", Email = "test@email.com", UserName = "test@email.com" });

        //    return userList;
        //}
    }
}
