using GLFManager.Api;
using GLFManager.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GLFManager.Tests.IntegrationTests
{
    public class UserAccountControllerTests : IClassFixture<IntegrationTest<Startup>>
    {
        private readonly IntegrationTest<Startup> _factory;
        private readonly HttpClient _client;
        public UserAccountControllerTests(IntegrationTest<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Return_BadRequest_If_EmailOrPassword_Is_Incorrect()
        {
            var loginCredentials = new LoginViewModel() { Email="shanelgmcguire@gmail.com", Password="Password1", ClientId="client" };

            var loginJson = JsonConvert.SerializeObject(loginCredentials, Formatting.Indented);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/useraccount/login");
            request.Content = new StringContent(loginJson, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
           

            ///////////////////////////////////////////////////////////////////////
            //string converted = JsonConvert.SerializeObject(loginCredentials, Formatting.Indented);
            //Console.WriteLine("!!!!!!!!!!!!!!!!!CONVERTED + " + converted);
            //var httpContent = new StringContent(converted);
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //var result = _client.PostAsync("/api/useraccount/login", httpContent).Result;
            // Arrange
            ////////////////////////////////////////////////////////////////////////
            //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //var login = await _client.PostAsync("/api/useraccount/login", new StringContent(JsonConvert.SerializeObject(loginCredentials), Encoding.UTF8, "application/json"));
            // Act
            // Assert
        }
    }
}
