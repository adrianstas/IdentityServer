using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core.Services.InMemory;

namespace STS.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser
                {
                    Username = "regular",
                    Password = "password",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705ddf",
                    Claims = new[]
                    {
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.GivenName, "Jan"),
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName, "Nowak"),
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.Address, "Katowice"),
                    }
                 },
                new InMemoryUser
                {
                    Username = "secret",
                    Password = "password",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705dd1",
                    Claims = new[]
                    {
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.GivenName, "Piotr"),
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName, "Kowalski"),
                        new Claim(IdentityServer3.Core.Constants.ClaimTypes.Address, "Gliwice")
                    }
                 }
            };
        }
    }
}