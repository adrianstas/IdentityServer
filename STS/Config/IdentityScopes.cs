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
                    Type = ScopeType.Resource
                }
            };
        }
    }
}