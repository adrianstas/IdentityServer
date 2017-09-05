using Client.MVC.Code.Constants;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using IdentityModel.Client;


namespace Client.MVC.Code
{
    public static class OidcClient
    {
        public static HttpClient GetClient(string address)
        {
            HttpClient client = new HttpClient();

            client.SetBearerToken(GetAccessToken());

            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static string GetAccessToken()
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

            var expiresAtFromClaims = DateTime.Parse(claimsIdentity.FindFirst("expires_at")
                .Value, null, DateTimeStyles.RoundtripKind);

            if (DateTime.Now.ToUniversalTime() < expiresAtFromClaims)
            {
                return claimsIdentity.FindFirst("access_token")
                    .Value;
            }
            
            var tokenEndpointClient = new TokenClient(IdentityConstants.TokenEndoint, "mvc_client_hybrid",
                IdentityConstants.MVCClientSecretHybrid);
            var requestRefreshTokenResponse =
                tokenEndpointClient.RequestRefreshTokenAsync(claimsIdentity.FindFirst("refresh_token")
                        .Value)
                    .Result;

            if (!requestRefreshTokenResponse.IsError)
            {
                var result = from claim in claimsIdentity.Claims
                    where claim.Type != "access_token" &&
                          claim.Type != "refresh_token" &&
                          claim.Type != "expires_at"
                    select claim;

                var claims = result.ToList();

                var expirationDateAsRoundtripString = DateTime.SpecifyKind(
                        DateTime.UtcNow.AddSeconds(requestRefreshTokenResponse.ExpiresIn), DateTimeKind.Utc)
                    .ToString("o");

                claims.Add(new Claim("access_token", requestRefreshTokenResponse.AccessToken));
                claims.Add(new Claim("expires_at", expirationDateAsRoundtripString));
                claims.Add(new Claim("refresh_token", requestRefreshTokenResponse.RefreshToken));

                var newIdentity = new ClaimsIdentity(claims, "Cookies", IdentityModel.JwtClaimTypes.Name,
                    IdentityModel.JwtClaimTypes.Role);

                HttpContext.Current.User = new ClaimsPrincipal(newIdentity);

                HttpContext.Current.Request.GetOwinContext()
                    .Authentication.SignIn(newIdentity);                

                return requestRefreshTokenResponse.AccessToken;
            }
            else
            {
                HttpContext.Current.Request.GetOwinContext()
                    .Authentication.SignOut();
                return string.Empty;
            }
        }
    }
}