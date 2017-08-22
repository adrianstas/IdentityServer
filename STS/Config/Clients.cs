using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace STS.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "client_credentials",
                    ClientName = "Client credentials client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("client_credential".Sha256())
                    },
                    Flow = Flows.ClientCredentials,
                    AllowedScopes = new List<string>
                    {
                        "regular",
                        "secret"
                    },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientId = "angular_client_implicit",
                    ClientName = "Implicit client",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>
                    {
                        "regular",
                        "secret"
                    },
                    AccessTokenType = AccessTokenType.Jwt,

                    // redirect = URI of the Angular application callback page
                    RedirectUris = new List<string>
                    {
                        "http://localhost:53364/callback.html"
                    }
                },
                new Client
                {
                    ClientId = "mvc_client_auth_code",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mvc_client_auth_code".Sha256())
                    },
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:59196/callback"
                    }
                },
                new Client
                {
                    ClientId = "mvc_client_hybrid",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mvc_client_hybrid".Sha256())
                    },
                    Flow = Flows.Hybrid,
                    AllowAccessToAllScopes = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:59196/Values/Index"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:59196/Values/Index"
                    }
                }
            };
        }
    }
}