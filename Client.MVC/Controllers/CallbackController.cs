using Client.MVC.Code.Constants;
using IdentityModel.Client;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.MVC.Controllers
{
    public class CallbackController : Controller
    {
        // GET: STSCallback
        public async Task<ActionResult> Index()
        {
            // get the authorization code from the query string
            var authCode = Request.QueryString["code"];

            // with the auth code, we can request an access token.
            var client = new TokenClient(
                IdentityConstants.TokenEndoint,
                "mvc_client_auth_code",
                 IdentityConstants.MVCClientSecretAuthCode);

            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                authCode,
                IdentityConstants.MVCAuthCodeCallback);

            Session["access_token"] = tokenResponse.AccessToken;

            // get the state (uri to return to)
            var state = Request.QueryString["state"];

            // redirect to the URI saved in state
            return Redirect(state);
        }
    }
}