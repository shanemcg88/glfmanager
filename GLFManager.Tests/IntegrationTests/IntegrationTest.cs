using GLFManager.Api;
using GLFManager.Api.Controllers;
using GLFManager.App;
using GLFManager.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.Tests.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            Console.WriteLine("ADD DB CONTEXT SECTION RAN");
                            options.UseInMemoryDatabase("testDb");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            //TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        //private async Task<string> GetJwtAsync()
        //{
            
        //}

        protected async Task LoginTest(LoginViewModel credentials)
        {
            var response = await TestClient.PostAsync("/api/useraccount/login", new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json"));

            var message = await response.Content.ReadAsStringAsync();

            Console.Write(message);

        }
    }
}
