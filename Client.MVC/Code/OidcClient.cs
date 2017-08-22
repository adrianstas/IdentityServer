using Client.MVC.Code.Constants;
using System;
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

            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var token = claimsIdentity?.FindFirst("access_token");
            if (token != null)
            {
                client.SetBearerToken(token.Value);
            }

            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}