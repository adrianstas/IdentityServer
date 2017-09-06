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
        public static HttpClient GetClient(string address)
        {
            HttpClient client = new HttpClient();
            var accessToken = RequestAccessTokenAuthorizationCode();
            client.SetBearerToken(accessToken);

            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static string RequestAccessTokenAuthorizationCode()
        {
            var access_token = HttpContext.Current.Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                return access_token;
            }

            // no token found - request one

            // we'll pass through the URI we want to return to as state
            var state = HttpContext.Current.Request.Url.OriginalString;

            var authorizeRequest = new AuthorizeRequest(
                IdentityConstants.AuthEndoint);

            var url = authorizeRequest.CreateAuthorizeUrl(IdentityConstants.MVCClientSecret, "code",
                "regular secret",
                IdentityConstants.MVCAuthCodeCallback, state);

            HttpContext.Current.Response.Redirect(url);

            return null;
        }
    }
}