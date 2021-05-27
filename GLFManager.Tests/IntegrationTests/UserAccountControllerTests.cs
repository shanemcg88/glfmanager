using GLFManager.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GLFManager.Tests.IntegrationTests
{
    public class UserAccountControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Return_BadRequest_If_EmailOrPassword_Is_Incorrect()
        {
            // Arrange

            var loginCredentials = new LoginViewModel() { Email = "test@email.com", Password = "test", ClientId = "client" };

            await LoginTest(loginCredentials);
            // Act

            // Assert
        }
    }
}
