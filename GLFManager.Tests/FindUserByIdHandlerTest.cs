using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GLFManager.Tests
{
    [Collection("Database")]
    public class FindUserByIdHandlerTest : DatabaseTestCase
    {
        private readonly FindUserByIdHandlerTest _handler;

        public FindUserByIdHandlerTest(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
           // _handler = new FindUserByIdHandlerTest(DbContext);
        }
    }
}
