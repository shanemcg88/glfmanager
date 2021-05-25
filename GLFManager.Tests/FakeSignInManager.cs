using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GLFManager.Tests
{
    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager()
            : base (new Mock<FakeUserManager>().Object,
                    new HttpContextAccessor(),
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object, 
                    null, 
                    null)
        {}
    }
}
