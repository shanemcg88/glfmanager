using System;
using System.Collections.Generic;
using System.Text;
using GLFManager.App;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GLFManager.Tests
{
    public abstract class DatabaseTestCase : IDisposable
    {
        public ApplicationDbContext DbContext { get; }
        protected DatabaseTestCase(DatabaseFixture databaseFixture)
        {
            var id = Guid.NewGuid().ToString().Replace("-", "");

            var databaseName = $"glfTestDb_{id}";

            using (var tmplConnection = new NpgsqlConnection(databaseFixture.Connection))
            {
                tmplConnection.Open();

                using (var cmd = new NpgsqlCommand($"CREATE DATABASE {databaseName} WITH TEMPLATE {databaseFixture.TemplateDatabaseName}", tmplConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            var connection = $"Host=localhost;Port=33030;Database={databaseName};User Id=devdbuser;Password=devdbpassword";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connection);

            DbContext = new ApplicationDbContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }
    }

}
