namespace ART.Infra.CrossCutting.WebApi
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public abstract class AuthenticatedApiControllerBase : NoAuthenticatedApiControllerBase
    {
        #region Properties

        protected Guid _applicationUserId
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var claim = identity.Claims.First(x => x.Type == "applicationUserId");
                var userId = Guid.Parse(claim.Value);
                return userId;
            }
        }

        #endregion Properties
    }
}