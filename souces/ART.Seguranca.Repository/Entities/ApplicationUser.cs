namespace ART.Seguranca.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntity<Guid>
    {
        #region Properties

        public ICollection<UsersInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}