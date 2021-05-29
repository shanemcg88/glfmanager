using GLFManager.Api;
using GLFManager.App.Repositories;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GLFManager.Tests.IntegrationTests
{
    public class UserAccountRepositoryTests : IClassFixture<IntegrationTest<Startup>>
    {
        private readonly IntegrationTest<Startup> _factory;
        private readonly HttpClient _client;

        public UserAccountRepositoryTests(IntegrationTest<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetUserByEmailFromDatabase()
        {
            // Arrange
            var loginEmail = "shanelgmcguire@gmail.com";
            var mockRepository = new Mock<IUserAccountRepository>();
            mockRepository.Setup(repo => repo.GetUserByEmail(loginEmail))
                .ReturnsAsync(new User { Email = "shanelgmcguire@gmail.com" });

            var repository = _factory.InitializeRepository();
            // Act
            var result = await repository.GetUserByEmail(loginEmail);

            // Assert
            Assert.NotNull(result);
        }
    }
}
