using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace GLFManager.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { JwtClaimTypes.Role })
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "glfapi",
                    DisplayName = "GLF API",
                    Scopes = { "glfapi.scope", JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("glfapi.scope", "GLF API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("UzKjRFnAHffxUFati8HMjSEzwMGgGHmN".Sha256())
                    },
                    AllowedScopes = { "glfapi.scope", "roles", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}
