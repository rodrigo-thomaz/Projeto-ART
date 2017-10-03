using System;
using System.Linq;
using System.Security.Claims;


namespace ART.Infra.CrossCutting.WebApi
{
    public abstract class AuthenticatedApiController : BaseApiController
    {
        protected Guid _userId
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var claim = identity.Claims.First(x => x.Type == "userId");
                var userId = Guid.Parse(claim.Value);
                return userId;
            }
        }
    }
}