using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Cors;
using API.Secret.Helpers;

namespace API.Secret.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/values")]
    public class SecretController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("SecretValues")]
        [Authorize(Roles = "SecretReader")]
        public IHttpActionResult ManagementValues()
        {
            TokenHelper.ListClaims();

            return Ok(new string[] { "Secret-value-1", "Secret-value-2" });
        }
    }
}
