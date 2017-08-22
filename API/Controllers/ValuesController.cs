using System.Web.Http;
using System.Web.Http.Cors;
using API.Regular.Helpers;

namespace API.Regular.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("ManagementValues")]
        public IHttpActionResult ManagementValues()
        {
            return Ok(new string[] { "Management-value-1", "Management-value-2" });
        }

        [HttpGet]
        [Authorize]
        [Route("RecruitmentValues")]
        public IHttpActionResult RecruitmentValues()
        {
            TokenHelper.ListClaims();

            return Ok(new string[] { "Recruitment-value-1", "Recruitment-value-2" });
        }

        [HttpGet]
        [Route("PublicValues")]
        public IHttpActionResult GetPublicValues()
        {
            return Ok(new string[] { "Public-value-1", "Public-value-2" });
        }
    }
}
