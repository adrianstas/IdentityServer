using System.Web.Http;

namespace API.Secret.Controllers
{
    public class SecretController : ApiController
    {
        [HttpGet]
        [Route("SecretValues")]
        public IHttpActionResult ManagementValues()
        {
            return Ok(new string[] { "Secret-value-1", "Secret-value-2" });
        }
    }
}
