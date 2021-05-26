using GLFManager.App;
using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Tests
{
    public class DatabaseFixture : IdentityDbContext<User>, IDisposable
    {
        private readonly DbContext _context;
        public string TemplateDatabaseName { get; }
        public string Connection { get; }

        public DatabaseFixture()
        {
            // Creates random database name for safety from duplicates
            var id = Guid.NewGuid().ToString().Replace("-", "");
            TemplateDatabaseName = $"glfTestDb_{id}";
            Connection = $"Host=localhost;Port=33030;Database={TemplateDatabaseName};User Id=devdbuser;Password=devdbpassword";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(Connection);

            _context = new ApplicationDbContext(optionsBuilder.Options);

            _context.Database.EnsureCreated();

            _context.Database.CloseConnection();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
