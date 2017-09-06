using Client.MVC.Code.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using IdentityModel.Client;

namespace Client.MVC.Code
{
    public static class OidcClient
    {
        public static HttpClient GetClient(string apiAddress)
        {
            HttpClient client = new HttpClient();

            var accessToken = RequestAccessTokenClientCredentials();
            client.SetBearerToken(accessToken);

            client.BaseAddress = new Uri(apiAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static string RequestAccessTokenClientCredentials()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get("ClientMVCCookie.ClientCredentials");
            if (cookie != null && cookie["access_token"] != null && !string.IsNullOrEmpty(cookie["access_token"]))
            {
                return cookie["access_token"];
            }

            var oAuth2Client = new TokenClient(
                      IdentityConstants.TokenEndoint,
                      "client_credentials",
                      IdentityConstants.MVCClientSecret);

            var tokenResponse = oAuth2Client.RequestClientCredentialsAsync("secret regular").Result;

            var httpCookie = HttpContext.Current.Response.Cookies["ClientMVCCookie.ClientCredentials"];
            httpCookie.Expires = DateTime.Now.AddMinutes(1);
            if (httpCookie != null)
                httpCookie["access_token"] = tokenResponse.AccessToken;

            return tokenResponse.AccessToken;
        }
    }
}