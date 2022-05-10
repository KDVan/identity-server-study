using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Duende_Identity_Server;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "full_access", displayName: "Full access to the API"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // machine to machine client
            new Client
            {
                ClientId = "client",

                // no interactive user, use the client id/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = {"full_access"}
            },

            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = {"https://localhost:5000/signin-oidc"},

                // where to redirect to after logout
                PostLogoutRedirectUris = {"https://localhost:5000/signout-callback-oidc"},
                
                // Enable refresh tokens for implicit flow
                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "full_access",
                }
            }
        };
}