namespace ART.Seguranca.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntity<Guid>
    {
        #region Properties

        #endregion Properties
    }
}