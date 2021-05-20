using System;
using Xunit;

namespace GLFManager.Tests
{
    public class UserAccountControllerTests
    {
        [Fact]
        public void AddingTwoNumbers()
        {
            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
