using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace STS.Config
{
    public static class IdentityScopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                new Scope
                {
                    Name = "regular",
                    DisplayName = "Regular scope",
                    Description = "Regular scope",
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = "secret",
                    DisplayName = "Secret scope",
                    Description = "Secret scope",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>()
                    {
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.GivenName),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.Role, true)
                    }
                },
                new Scope
                {
                    Name = "additional",
                    DisplayName = "Additional scope",
                    Description = "Additional scope",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>()
                    {
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.NickName),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.WebSite)
                    }
                }
            };
        }
    }
}