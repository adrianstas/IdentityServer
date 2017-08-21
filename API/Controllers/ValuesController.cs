using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Regular.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("ManagementValues")]
        public IHttpActionResult ManagementValues()
        {
            return Ok(new string[] { "Management-value-1", "Management-value-2" });
        }

        [HttpGet]
        [Route("RecruitmentValues")]
        public IHttpActionResult RecruitmentValues()
        {
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
