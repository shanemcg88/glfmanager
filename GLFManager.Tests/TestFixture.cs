using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GLFManager.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient _client;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(@"F:\webdev\glfManager\GLFManager")
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}
