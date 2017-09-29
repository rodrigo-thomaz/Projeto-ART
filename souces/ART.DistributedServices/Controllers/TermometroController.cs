using ART.Infra.CrossCutting.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ART.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/termometro")]
    public class TermometroController : BaseApiController
    {
        [Route("")]
        public IEnumerable<object> Get()
        {            
            //string token = "";
            //Microsoft.Owin.Security.AuthenticationTicket ticket = Startup.OAuthBearerOptions.AccessTokenFormat.Unprotect(token);

            var identity = User.Identity as ClaimsIdentity;
           
            return identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}
