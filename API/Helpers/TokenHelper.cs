using System.Linq;
using System.Security.Claims;
using System.Web;

namespace API.Regular.Helpers
{
    public class TokenHelper
    {
        public static void ListClaims()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            identity.Claims.ToList().ForEach(claim=>System.Diagnostics.Debug.WriteLine($"claim type: '{claim.Type}', value:'{claim.Value}'"));
        }
    }
}