using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GLFManager.Tests
{
    [CollectionDefinition("Database")]
    public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
    {
    }
}
