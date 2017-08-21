using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Secret.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/values")]
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
